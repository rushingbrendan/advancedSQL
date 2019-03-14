namespace WindowsFormsApp1
{
    partial class Foodmart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ProductClass = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Products = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SalesCities = new System.Windows.Forms.ListBox();
            this.updateChartButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(417, 82);
            this.chart1.MaximumSize = new System.Drawing.Size(574, 364);
            this.chart1.MinimumSize = new System.Drawing.Size(574, 364);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(574, 364);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // ProductClass
            // 
            this.ProductClass.FormattingEnabled = true;
            this.ProductClass.Location = new System.Drawing.Point(44, 63);
            this.ProductClass.Name = "ProductClass";
            this.ProductClass.Size = new System.Drawing.Size(160, 134);
            this.ProductClass.TabIndex = 1;
            this.ProductClass.SelectedIndexChanged += new System.EventHandler(this.ProductClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Product Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Product";
            // 
            // Products
            // 
            this.Products.FormattingEnabled = true;
            this.Products.Location = new System.Drawing.Point(44, 233);
            this.Products.Name = "Products";
            this.Products.Size = new System.Drawing.Size(160, 134);
            this.Products.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sales City";
            // 
            // SalesCities
            // 
            this.SalesCities.FormattingEnabled = true;
            this.SalesCities.Location = new System.Drawing.Point(44, 399);
            this.SalesCities.Name = "SalesCities";
            this.SalesCities.Size = new System.Drawing.Size(160, 121);
            this.SalesCities.TabIndex = 3;
            // 
            // updateChartButton
            // 
            this.updateChartButton.Location = new System.Drawing.Point(242, 478);
            this.updateChartButton.Name = "updateChartButton";
            this.updateChartButton.Size = new System.Drawing.Size(154, 42);
            this.updateChartButton.TabIndex = 7;
            this.updateChartButton.Text = "Update Chart";
            this.updateChartButton.UseVisualStyleBackColor = true;
            this.updateChartButton.Click += new System.EventHandler(this.updateChartButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(256, 102);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(103, 307);
            this.clearButton.TabIndex = 8;
            this.clearButton.Text = "Clear All Selections";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Foodmart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 574);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.updateChartButton);
            this.Controls.Add(this.SalesCities);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Products);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProductClass);
            this.Controls.Add(this.chart1);
            this.Name = "Foodmart";
            this.Text = "Advanced SQL Foodmart Chart - Brendan Rushing";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ListBox ProductClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox Products;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox SalesCities;
        private System.Windows.Forms.Button updateChartButton;
        private System.Windows.Forms.Button clearButton;
    }
}

