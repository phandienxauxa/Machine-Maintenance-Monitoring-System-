namespace demo
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView2 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView3 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram2 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView4 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView5 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ChartTitle chartTitle3 = new DevExpress.XtraCharts.ChartTitle();
            chartControl1 = new DevExpress.XtraCharts.ChartControl();
            arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            labelComponent2 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
            circularGauge1 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            arcScaleRangeBarComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            chartControl2 = new DevExpress.XtraCharts.ChartControl();
            progBattery = new DevExpress.XtraEditors.ProgressBarControl();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            chartControl3 = new DevExpress.XtraCharts.ChartControl();
            alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(components);
            ((System.ComponentModel.ISupportInitialize)chartControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotDiagram1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)arcScaleComponent2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)labelComponent2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)circularGauge1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)arcScaleRangeBarComponent2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotDiagram2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)progBattery.Properties).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xyDiagram1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series7).BeginInit();
            SuspendLayout();
            // 
            // chartControl1
            // 
            chartControl1.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            swiftPlotDiagram1.AxisX.Title.Text = "Thời điểm thu mẫu";
            swiftPlotDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.Title.Text = "Gia tốc (m/s^2)";
            swiftPlotDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.EnableAxisXScrolling = true;
            swiftPlotDiagram1.EnableAxisXZooming = true;
            swiftPlotDiagram1.EnableAxisYScrolling = true;
            swiftPlotDiagram1.EnableAxisYZooming = true;
            chartControl1.Diagram = swiftPlotDiagram1;
            chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            chartControl1.Location = new System.Drawing.Point(3, 240);
            chartControl1.Name = "chartControl1";
            series1.Name = "truc_x";
            series1.SeriesID = 1;
            swiftPlotSeriesView1.LineStyle.Thickness = 3;
            series1.View = swiftPlotSeriesView1;
            series2.Name = "truc_y";
            series2.SeriesID = 2;
            swiftPlotSeriesView2.LineStyle.Thickness = 3;
            series2.View = swiftPlotSeriesView2;
            series3.Name = "truc_z";
            series3.SeriesID = 3;
            swiftPlotSeriesView3.LineStyle.Thickness = 3;
            series3.View = swiftPlotSeriesView3;
            chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series1,
    series2,
    series3
    };
            chartControl1.Size = new System.Drawing.Size(866, 637);
            chartControl1.TabIndex = 0;
            chartTitle1.Text = "Biểu đồ Rung Động";
            chartTitle1.TitleID = 0;
            chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle1 });
            chartControl1.DoubleClick += chartControl1_DoubleClick;
            // 
            // arcScaleComponent2
            // 
            arcScaleComponent2.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            arcScaleComponent2.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            arcScaleComponent2.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            arcScaleComponent2.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            arcScaleComponent2.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 8.5F);
            arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A");
            arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(0F, 0F);
            arcScaleComponent2.EndAngle = 90F;
            arcScaleComponent2.MajorTickCount = 0;
            arcScaleComponent2.MajorTickmark.FormatString = "{0:F0}";
            arcScaleComponent2.MajorTickmark.ShapeOffset = -14F;
            arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1;
            arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            arcScaleComponent2.MaxValue = 100F;
            arcScaleComponent2.MinorTickCount = 0;
            arcScaleComponent2.MinorTickmark.ShapeOffset = -7F;
            arcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2;
            arcScaleComponent2.Name = "scale1";
            arcScaleComponent2.StartAngle = -270F;
            arcScaleComponent2.Value = 20F;
            // 
            // labelComponent2
            // 
            labelComponent2.AppearanceText.DXFont = new DevExpress.Drawing.DXFont("Segoe UI", 27.75F);
            labelComponent2.Name = "circularGauge1_Label1";
            labelComponent2.Size = new System.Drawing.SizeF(140F, 60F);
            labelComponent2.Text = "910";
            labelComponent2.ZOrder = -1001;
            // 
            // circularGauge1
            // 
            circularGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 511, 229);
            circularGauge1.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[] { labelComponent2 });
            circularGauge1.Name = "circularGauge1";
            circularGauge1.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] { arcScaleComponent2 });
            // 
            // arcScaleRangeBarComponent2
            // 
            arcScaleRangeBarComponent2.ArcScale = arcScaleComponent2;
            arcScaleRangeBarComponent2.Name = "circularGauge1_RangeBar2";
            arcScaleRangeBarComponent2.RoundedCaps = true;
            arcScaleRangeBarComponent2.ShowBackground = true;
            arcScaleRangeBarComponent2.StartOffset = 80F;
            arcScaleRangeBarComponent2.ZOrder = -10;
            // 
            // chartControl2
            // 
            swiftPlotDiagram2.AxisX.Title.Text = "Thời gian thực";
            swiftPlotDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram2.AxisY.Title.Text = "GIá trị đo";
            swiftPlotDiagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram2.EnableAxisXScrolling = true;
            swiftPlotDiagram2.EnableAxisXZooming = true;
            swiftPlotDiagram2.EnableAxisYScrolling = true;
            swiftPlotDiagram2.EnableAxisYZooming = true;
            chartControl2.Diagram = swiftPlotDiagram2;
            chartControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            chartControl2.Location = new System.Drawing.Point(3, 3);
            chartControl2.Name = "chartControl2";
            series4.Name = "do_am";
            series4.SeriesID = 0;
            swiftPlotSeriesView4.LineStyle.Thickness = 3;
            series4.View = swiftPlotSeriesView4;
            series5.Name = "nhiet_do";
            series5.SeriesID = 1;
            swiftPlotSeriesView5.LineStyle.Thickness = 3;
            series5.View = swiftPlotSeriesView5;
            chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series4,
    series5
    };
            chartControl2.Size = new System.Drawing.Size(866, 231);
            chartControl2.TabIndex = 1;
            chartTitle2.Text = "Biểu đồ Nhiệt độ và Độ ẩm";
            chartTitle2.TitleID = 0;
            chartControl2.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle2 });
            // 
            // progBattery
            // 
            progBattery.Dock = System.Windows.Forms.DockStyle.Fill;
            progBattery.Location = new System.Drawing.Point(875, 3);
            progBattery.Name = "progBattery";
            progBattery.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            progBattery.Properties.ShowTitle = true;
            progBattery.Size = new System.Drawing.Size(369, 231);
            progBattery.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.9463348F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0536671F));
            tableLayoutPanel1.Controls.Add(progBattery, 1, 0);
            tableLayoutPanel1.Controls.Add(chartControl1, 0, 1);
            tableLayoutPanel1.Controls.Add(chartControl2, 0, 0);
            tableLayoutPanel1.Controls.Add(chartControl3, 1, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.96011F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.03989F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1247, 880);
            tableLayoutPanel1.TabIndex = 3;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // chartControl3
            // 
            xyDiagram1.AxisX.Title.Text = "Chế độ kích hoạt ";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Title.Text = "Số lần";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            chartControl3.Diagram = xyDiagram1;
            chartControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            chartControl3.Location = new System.Drawing.Point(875, 240);
            chartControl3.Name = "chartControl3";
            series6.Name = "Thức định kỳ";
            series6.SeriesID = 0;
            series7.Name = "Thức do rung";
            series7.SeriesID = 1;
            chartControl3.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series6,
    series7
    };
            chartControl3.Size = new System.Drawing.Size(369, 637);
            chartControl3.TabIndex = 3;
            chartTitle3.Text = "Số lần đánh thức  ";
            chartTitle3.TitleID = 0;
            chartControl3.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle3 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1247, 880);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)swiftPlotDiagram1).EndInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)series1).EndInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)series2).EndInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)series3).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)arcScaleComponent2).EndInit();
            ((System.ComponentModel.ISupportInitialize)labelComponent2).EndInit();
            ((System.ComponentModel.ISupportInitialize)circularGauge1).EndInit();
            ((System.ComponentModel.ISupportInitialize)arcScaleRangeBarComponent2).EndInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotDiagram2).EndInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView4).EndInit();
            ((System.ComponentModel.ISupportInitialize)series4).EndInit();
            ((System.ComponentModel.ISupportInitialize)swiftPlotSeriesView5).EndInit();
            ((System.ComponentModel.ISupportInitialize)series5).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartControl2).EndInit();
            ((System.ComponentModel.ISupportInitialize)progBattery.Properties).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)xyDiagram1).EndInit();
            ((System.ComponentModel.ISupportInitialize)series6).EndInit();
            ((System.ComponentModel.ISupportInitialize)series7).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartControl3).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent2;
        private DevExpress.XtraGauges.Win.Base.LabelComponent labelComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent arcScaleRangeBarComponent2;
        private DevExpress.XtraCharts.ChartControl chartControl2;
        private DevExpress.XtraEditors.ProgressBarControl progBattery;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraCharts.ChartControl chartControl3;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
    }
}

