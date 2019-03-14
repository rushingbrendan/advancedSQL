using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> ProductClassItems = new List<string>();
        List<string> ProductItems = new List<string>();
        List<string> SalesCityItems = new List<string>();

        double[]monthlySales = new double[12];
        string connectionString = ConfigurationManager.AppSettings["databaseConnectionString"].ToString();


        string QueryString = "";
        bool QueryStringUpdated = false;
        bool ApplicationFirstRun = true;

        string ChartSeriesName = "Sales";
        bool ChartExists = false;
        

        public Form1()
        {
            InitializeComponent();

            try
            {
                updateListBoxes();
            }
            catch (Exception)
            {

            }

            //default all list boxes to no items selected
            ProductClass.SetSelected(0, false);
            Products.SetSelected(0, false);
            SalesCities.SetSelected(0, false);

            chart1.Titles.Add("Foodmart 1998 Sales");
            ApplicationFirstRun = false;


        }

        private void updateListBoxes()
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader rdr;
            cmd.Connection = conn;
            cmd.CommandText = "SELECT product_subcategory FROM product_class";
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string temp = rdr[0].ToString();
                ProductClassItems.Add(rdr[0].ToString());
            }
            conn.Close();
            ProductClass.DataSource = ProductClassItems;

            cmd.CommandText = "SELECT product_name FROM product";
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string temp = rdr[0].ToString();
                ProductItems.Add(rdr[0].ToString());
            }
            conn.Close();
            Products.DataSource = ProductItems;

            cmd.CommandText = "SELECT store_city FROM store";
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string temp = rdr[0].ToString();
                SalesCityItems.Add(rdr[0].ToString());
            }
            conn.Close();
            SalesCities.DataSource = SalesCityItems;


            rdr.Close();
            conn.Close();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void updateChartButton_Click(object sender, EventArgs e)
        {
            //get store_id
            int i = ProductClass.SelectedIndex; // RETURNS 1.

            int test = 0;
            //get product_id

            //get product_class_id

            //buildChartData();

            buildQueryString();

            updateMonthlySales();

            if (ChartExists == true)
            {
                ClearChartData();
            }

            buildChartData();

            ChartExists = true;
        }

        private void buildChartData()
        {
            chart1.Series.Add(ChartSeriesName);            
            chart1.ChartAreas[0].AxisX.Interval = 1;   //show all months
            chart1.ChartAreas[0].AxisX.Title = "1998 Months";
            chart1.ChartAreas[0].AxisY.Title = "Sales";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "$ {}";
            chart1.ChartAreas[0].RecalculateAxesScale();

            //chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;



            chart1.Series[ChartSeriesName].Points.AddXY("January", monthlySales[0]);
            chart1.Series[ChartSeriesName].Points.AddXY("February", monthlySales[1]);
            chart1.Series[ChartSeriesName].Points.AddXY("March", monthlySales[2]);
            chart1.Series[ChartSeriesName].Points.AddXY("April", monthlySales[3]);
            chart1.Series[ChartSeriesName].Points.AddXY("May", monthlySales[4]);
            chart1.Series[ChartSeriesName].Points.AddXY("June", monthlySales[5]);
            chart1.Series[ChartSeriesName].Points.AddXY("July", monthlySales[6]);
            chart1.Series[ChartSeriesName].Points.AddXY("August", monthlySales[7]);
            chart1.Series[ChartSeriesName].Points.AddXY("September", monthlySales[8]);
            chart1.Series[ChartSeriesName].Points.AddXY("October", monthlySales[9]);
            chart1.Series[ChartSeriesName].Points.AddXY("November", monthlySales[10]);
            chart1.Series[ChartSeriesName].Points.AddXY("December", monthlySales[11]);

        }

        private void buildQueryString()
        {
            bool itemAddedToString = false;
            string currentQueryString = "SELECT SUM(store_sales) FROM sales_fact_1998 INNER JOIN time_by_day ON sales_fact_1998.time_id = time_by_day.time_id INNER JOIN store ON sales_fact_1998.store_id = store.store_id  INNER JOIN product ON sales_fact_1998.product_id = product.product_id";

            ChartSeriesName = "All Sales";

            //Product class is selected
           if (ProductClass.SelectedIndex != -1)
            {
                currentQueryString += $" WHERE product.product_class_id = {ProductClass.SelectedIndex + 1}";
                itemAddedToString = true;
                QueryStringUpdated = true;                
            }

           //Product is selected
           if (Products.SelectedIndex != -1)
            {
                if (itemAddedToString == false)
                {
                    currentQueryString += $" WHERE product.product_id = {Products.SelectedIndex + 1}";
                }
                else
                {
                    currentQueryString += $" AND product.product_id = {Products.SelectedIndex + 1}";
                }

                itemAddedToString = true;
                QueryStringUpdated = true;
            }

            //Sales city is selected
            if (SalesCities.SelectedIndex != -1)
            {
                if (itemAddedToString == false)
                {
                    currentQueryString += $" WHERE store.store_id = {SalesCities.SelectedIndex + 1}";
                }
                else
                {
                    currentQueryString += $" AND store.store_id = {SalesCities.SelectedIndex + 1}";
                }
                QueryStringUpdated = true;
            }

            //update class query string
            QueryString = currentQueryString;


            
        }

        private void updateMonthlySales()
        {
            //get data for each month
            for (int i = 1; i < 13; i++)
            {
                string currentQueryString = "";
                int monthCounter = i - 1;
                
                if (QueryStringUpdated == false)
                {
                    currentQueryString = QueryString + $" WHERE time_by_day.month_of_year = {i}";
                }
                else
                {
                    currentQueryString = QueryString + $" AND time_by_day.month_of_year = {i}";
                }

                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataReader rdr;
                cmd.Connection = conn;
                cmd.CommandText = currentQueryString;
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    double currentTotal = 0;
                    Double.TryParse(rdr[0].ToString(), out currentTotal);
                    monthlySales[monthCounter] = currentTotal;
                }
                conn.Close();
                rdr.Close();

            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ProductClass.SelectedIndex = -1;
            SalesCities.SelectedIndex = -1;
            Products.SelectedIndex = -1;

            try
            {
                updateListBoxes();
            }
            catch (Exception)
            {

            }

        }

        private void ProductClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ApplicationFirstRun == false)
            {
                //clear ProductItems
                ProductItems.Clear();
                Products.DataSource = null;
                Products.Items.Clear();

                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataReader rdr;
                cmd.Connection = conn;

                cmd.CommandText = $"SELECT product_name FROM product WHERE product_class_id = {ProductClass.SelectedIndex + 1}";
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string temp = rdr[0].ToString();
                    ProductItems.Add(rdr[0].ToString());
                }
                conn.Close();
                Products.DataSource = ProductItems;



                rdr.Close();

                Products.SelectedIndex = -1;
            }
           
        }

        private void ClearChartData()
        {

            chart1.Series.Clear();

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();                
            }

            ChartExists = false;
        }
    }
}
