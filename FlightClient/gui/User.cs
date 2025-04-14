using System.Data;
using System.Windows.Forms;
using FlightModel;
using FlightServices;

namespace LabMPP;

public partial class User : Form, IFlightObserver
{
    //private Service service;
    private IFlightServices server;
    private Manager manager;
    private Trip tripForReservation;
    private long _selectedTrip;
    
    public User()
    {
        InitializeComponent();
    }
    
    public void SetServer(IFlightServices server)
    {
        this.server = server;
    }
        
    public void SetLoggedUser(Manager loggedUser)
    {
        this.manager = loggedUser;
        LoadData();
    }

    private void LoadData()
    {
        dataGridView1.AutoGenerateColumns = true;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

        IEnumerable<Trip> trips = server.GetAllTrips();
        List<Trip> tripList = trips.ToList();
        DataTable table = new DataTable();
        
        table.Columns.Add("Id", typeof(long));
        table.Columns.Add("Destination", typeof(string));
        table.Columns.Add("DepartureDate", typeof(DateTime));
        table.Columns.Add("Airport", typeof(string));
        table.Columns.Add("NoOfAvailableSeats", typeof(int));

        foreach (Trip trip in tripList)
        {
            if (trip.NoOfSeatsAvailable == 0)
                continue;
            table.Rows.Add(trip.Id, trip.Destination, trip.DepartureTime, trip.Aeroport, trip.NoOfSeatsAvailable);
        }
        dataGridView1.DataSource = table;
        dataGridView1.DataBindingComplete += (s, e) =>
        {
            int rowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);
            int rowHeight = dataGridView1.RowTemplate.Height;
            int headerHeight = dataGridView1.ColumnHeadersHeight;

            int totalHeight = (rowHeight * rowCount) + headerHeight + 5;
            dataGridView1.Height = Math.Min(totalHeight, 500);
        };

        foreach (Trip trip in tripList)
        {
            if (trip.NoOfSeatsAvailable == 0)
                continue;
            if(!comboBox1.Items.Contains(trip.Destination))
                comboBox1.Items.Add(trip.Destination);
        }

    }

    public void ReservationAdded(Reservation reservation)
    {
        this.BeginInvoke((MethodInvoker) delegate
        {
            Console.WriteLine("Reservation added: " + reservation);
            LoadData();
            string destination = comboBox1.Text;
            DateTime date = dateTimePicker1.Value;

            if (destination.Length != 0 && date > DateTime.Now)
            {
                IEnumerable<Trip> trips = server.SearchTripsByDestination(destination, date);
                List<Trip> tripList = trips.ToList();
                if(tripList.Count == 0)
                    MessageBox.Show("No trips found");
                DataTable table = new DataTable();

                table.Columns.Add("Id", typeof(long));
                table.Columns.Add("Destination", typeof(string));
                table.Columns.Add("DepartureTime", typeof(string));
                table.Columns.Add("NoOfSeats", typeof(int));

                foreach (Trip trip in tripList)
                {
                    if(trip.NoOfSeatsAvailable == 0)
                        continue;
                    table.Rows.Add(trip.Id, trip.Destination, trip.DepartureTime.TimeOfDay.ToString(), trip.NoOfSeatsAvailable);
                }

                dataGridView2.DataSource = table;
                dataGridView2.DataBindingComplete += (s, e) =>
                {
                    int rowCount = dataGridView2.Rows.GetRowCount(DataGridViewElementStates.Visible);
                    int rowHeight = dataGridView2.RowTemplate.Height;
                    int headerHeight = dataGridView2.ColumnHeadersHeight;

                    int totalHeight = (rowHeight * rowCount) + headerHeight + 5;
                    dataGridView2.Height = Math.Min(totalHeight, 500);
                };
            }
        });
    }

    private void reservationButton_Click(object sender, EventArgs e)
    {
        if (dataGridView2.SelectedRows.Count > 0)
        {
            var cellValue = dataGridView2.SelectedRows[0].Cells[0].Value;
            if (cellValue != null)
            {
                _selectedTrip = Convert.ToInt64(cellValue);
            }
        }
        else
        {
            MessageBox.Show("No selected trip");
        }

        Trip trip = server.SearchTripById(_selectedTrip);

        if (_selectedTrip != null && seatsTextBox.Text != "" && nameTextBox.Text != "")
        {
            int noOfSeats = Convert.ToInt32(seatsTextBox.Text);
            string name = nameTextBox.Text;
            Reservation reservation = new Reservation(trip, noOfSeats, name);
            server.AddReservation(reservation);
            //long noSeatsAvailable = server.GetNoOfSeatsAvailable(trip);
            //long newSeats = noSeatsAvailable - noOfSeats;
            //trip.NoOfSeatsAvailable = newSeats;
            //server.ModifyTrip(trip);
            MessageBox.Show("Successfully added reservation");
            seatsTextBox.Text = "";
            nameTextBox.Text = "";
        }
        searchButton_Click(sender, null);
        //LoadData();

    }

    private void searchButton_Click(object sender, EventArgs e)
    {
        string destination = comboBox1.Text;
        DateTime date = dateTimePicker1.Value;

        if (destination.Length != 0 && date > DateTime.Now)
        {
            IEnumerable<Trip> trips = server.SearchTripsByDestination(destination, date);
            List<Trip> tripList = trips.ToList();
            if(tripList.Count == 0)
                MessageBox.Show("No trips found");
            DataTable table = new DataTable();

            table.Columns.Add("Id", typeof(long));
            table.Columns.Add("Destination", typeof(string));
            table.Columns.Add("DepartureTime", typeof(string));
            table.Columns.Add("NoOfSeats", typeof(int));

            foreach (Trip trip in tripList)
            {
                if(trip.NoOfSeatsAvailable == 0)
                    continue;
                table.Rows.Add(trip.Id, trip.Destination, trip.DepartureTime.TimeOfDay.ToString(), trip.NoOfSeatsAvailable);
            }

            dataGridView2.DataSource = table;
            dataGridView2.DataBindingComplete += (s, e) =>
            {
                int rowCount = dataGridView2.Rows.GetRowCount(DataGridViewElementStates.Visible);
                int rowHeight = dataGridView2.RowTemplate.Height;
                int headerHeight = dataGridView2.ColumnHeadersHeight;

                int totalHeight = (rowHeight * rowCount) + headerHeight + 5;
                dataGridView2.Height = Math.Min(totalHeight, 500);
            };
        }
        else
        {
            MessageBox.Show("Please enter a destination and a valid date!");
        }
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
        try
        {
            server.Logout(manager, this);
            MessageBox.Show("User logged out.");
            Application.Exit();
        }
        catch (FlightException ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

}