using FlightModel;
using LabMPP.repository.databases;
using LabMPP.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlightPersistance.entityFramework;

public class ManagerEFRepo : IManagerRepo
    {
        private readonly MyDbContext _context;
        public ManagerEFRepo(MyDbContext context)
        {
            _context = context;
        }

        public Manager FindOne(long id)
        {
            return _context.Managers.Find(id);
        }

        public IEnumerable<Manager> FindAll()
        {
            return _context.Managers.ToList(); 
        }

        public Manager Save(Manager manager)
        {
            _context.Managers.Add(manager); 
            _context.SaveChangesAsync();
            return manager;
        }

        public Manager Delete(long id)
        {
            var managerToDelete = FindOne(id);
            if (managerToDelete != null)
            {
                _context.Managers.Remove(managerToDelete); 
                _context.SaveChangesAsync(); 
            }
            return managerToDelete;
        }

        public Manager Update(Manager manager)
        {
            _context.Managers.Update(manager); 
            _context.SaveChangesAsync();
            return manager;
        }
    }