/*  Course:         PROG3070 - Advanced SQL
 *  Assignment:     SQL Database data visualization in chart
 *  Programmer:     Brendan Rushing 6020895
 *  Date:           3/14/2019
 *  
 *  Description:
 *      Display Foodmart data from SQL Server database in C# Windows Form Chart.
 *      
        Requirements
        You will write a C# program (Forms or ASP.NET) that uses MS Chart to display a graph. Here are the
        requirements:
        1. You will use the Foodmart database to display the store sales data from the sales_fact_1998
        table.
        2. The graph should be a column chart.
        3. The x-axis should be the Months of the year (as month names, not numbers).
        4. Appropriate titles (x-axis, y-axis, Chart title, etc.) must be used.
        5. You should be able to choose the chart by Product Class, Product and Sales City. Each of the
        selection criteria can be “any one” or “all”. In other words, you can filter by any one Product
        Class, or all Product Classes at a time. No need to select a specific set of Product Classes. These
        choices are likely to be in drop-down list boxes and always visible on the form.
        Note 1: The list of available Products is specific to the Product Classes chosen: when the Product
        Class changes, the list of products might need to change.
        Note 2: The Product Class, Product and Sales City must be displayed by name.
        Note 3: Use the Product_Subcategory column in the Product_Class table as the text description.
        6. Choose an appropriate method to refresh the graph. For example, you can either refresh each
        time a new choice is made, or you can make your choices and have a button available to do the
        refresh.
        7. Make sure the connection string is specified in the app.config (or web.config) file. You do not
        need any tool to change it. Assume it is editable using any editor. You can use the SQL Client
        data objects (instead of OleDB).
        8. The user interface is important. It should be easy to use and have no ambiguous information
        presented.
 * 
 * 
 * 
 * */


//includes
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
//eo includes

namespace WindowsFormsApp1
{
    /// <summary>
    /// Foodmart Chart Class
    /// </summary>
    public partial class Foodmart : Form
    {
        List<string> ProductClassItems = new List<string>();    //list of product classes
        List<string> ProductItems = new List<string>();         //list of products
        List<string> SalesCityItems = new List<string>();       //list of sales cities

        double[]monthlySales = new double[12];                  //array of monthly sales

        string connectionString = ConfigurationManager.AppSettings["databaseConnectionString"].ToString();  //connection string

        string QueryString = "";        //query string
        string ChartSeriesName = "Sales";   //chart name

        bool QueryStringUpdated = false;
        bool ApplicationFirstRun = true;
        bool ChartExists = false;
        

       /// <summary>
       /// Constructor
       /// </summary>
        public Foodmart()
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

            //add title
            chart1.Titles.Add("Foodmart 1998 Sales");
            ApplicationFirstRun = false;            
        }

        /// <summary>
        /// Updates list boxes in form with item names from database
        /// </summary>
        private void updateListBoxes()
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader rdr;
            cmd.Connection = conn;

            //get product classes
            cmd.CommandText = "SELECT product_subcategory FROM product_class";
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //add to product classes list
                ProductClassItems.Add(rdr[0].ToString());
            }
            conn.Close();
            ProductClass.DataSource = ProductClassItems;

            //get product names
            cmd.CommandText = "SELECT product_name FROM product";
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //add to product names list
                ProductItems.Add(rdr[0].ToString());
            }
            conn.Close();
            Products.DataSource = ProductItems;

            //get city store names
            cmd.CommandText = "SELECT store_city FROM store";
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //add to city store names list
                SalesCityItems.Add(rdr[0].ToString());
            }
            conn.Close();
            SalesCities.DataSource = SalesCityItems;
            rdr.Close();
        }


        /// <summary>
        /// Button handler to fill in chart with data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateChartButton_Click(object sender, EventArgs e)
        {
            //get store_id
            int i = ProductClass.SelectedIndex; // RETURNS 1.

            //build query string bases on selected items
            buildQueryString();

            //update monthly sales from database
            updateMonthlySales();

            //clear chart if exists
            if (ChartExists == true)
            {
                ClearChartData();
            }

            //update chart
            buildChartData();

            ChartExists = true;
        }

        /// <summary>
        /// Fill in all chart data
        /// </summary>
        private void buildChartData()
        {
            chart1.Series.Add(ChartSeriesName);            
            chart1.ChartAreas[0].AxisX.Interval = 1;   //show all months
            chart1.ChartAreas[0].AxisX.Title = "1998 Months";
            chart1.ChartAreas[0].AxisY.Title = "Sales";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "$ {}";
            chart1.ChartAreas[0].RecalculateAxesScale();

            //chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;


            //get monthly data from class
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

        /// <summary>
        /// Build query string based on list items selected
        /// </summary>
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

        /// <summary>
        /// Update monthly sales from database
        /// </summary>
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

        /// <summary>
        /// Clear selected list items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Update products based on selected product class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Clear chart data
        /// </summary>
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
