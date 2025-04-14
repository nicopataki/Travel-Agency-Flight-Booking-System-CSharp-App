using LabMPP.domain;

namespace LabMPP.repository;

public interface IRepository<ID, E> where E: Entity<ID>
{
    /*
     * Find the entity with the given id
     */
    E FindOne(ID id);
    
    /*
     * @return all entities
     */
    IEnumerable<E> FindAll();

    /*
     * Save an entity in the repository
     */
    E Save(E entity);
    
    /*
     * Delete an entity with the given id
     */
    E Delete(ID id);
    
    /*
     * Update an entity
     */
    E Update(E entity);
}