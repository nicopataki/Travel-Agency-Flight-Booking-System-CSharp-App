using System.Windows.Forms;
using FlightModel;
using FlightServices;

namespace LabMPP;

public partial class LogIn : Form
{
    private User mainForm;
    private Manager manager;
    private IFlightServices server;
    
    //private Service service;
    /*public LogIn(Service service)
    {
        this.service = service;
        InitializeComponent();
    }*/
    
    public void SetServer(IFlightServices server)
    {
        this.server = server;
    }
        
    public void SetMainForm(User mainForm)
    {
        this.mainForm = mainForm;
    }
        
    public LogIn()
    {
        InitializeComponent();
    }

    private void LogIn_Click(object sender, EventArgs e)
    {
        string name = UsernameTextBox.Text;
        string password = PasswordTextBox.Text;
        //Manager manager = service.GetManagerByName(username);
        manager = new Manager(name, password);

        if (manager == null)
        {
            MessageBox.Show("Error: LoggedUser is null");
            return;
        }

        try
        {
            server.Login(manager, mainForm);
            Console.WriteLine("User successfully logged in!");
            mainForm.SetLoggedUser(manager);
            mainForm.Show();
            this.Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to login: " + ex.Message);
        }
    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

}