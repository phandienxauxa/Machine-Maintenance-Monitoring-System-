namespace demo
{
    partial class FormFFT
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            chartControlFFT = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)chartControlFFT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xyDiagram1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)series1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splineSeriesView1).BeginInit();
            SuspendLayout();
            // 
            // chartControlFFT
            // 
            chartControlFFT.AppearanceNameSerializable = "Light";
            xyDiagram1.AxisX.Title.Text = "Tần số (Hz)";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Title.Text = "Biên độ (Độ lớn)";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            chartControlFFT.Diagram = xyDiagram1;
            chartControlFFT.Dock = System.Windows.Forms.DockStyle.Fill;
            chartControlFFT.Location = new System.Drawing.Point(0, 0);
            chartControlFFT.Name = "chartControlFFT";
            chartControlFFT.PaletteBaseColorNumber = 1;
            chartControlFFT.PaletteName = "Blue II";
            series1.Name = "Phổ tần số rung động";
            series1.SeriesID = 0;
            splineSeriesView1.SplineAlgorithm = DevExpress.XtraCharts.SplineAlgorithm.Cardinal;
            series1.View = splineSeriesView1;
            chartControlFFT.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series1
    };
            chartControlFFT.Size = new System.Drawing.Size(894, 464);
            chartControlFFT.TabIndex = 0;
            chartTitle1.Text = "Phân tích phổ FFT";
            chartTitle1.TitleID = 1;
            chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chartControlFFT.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle1 });
            // 
            // FormFFT
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(894, 464);
            Controls.Add(chartControlFFT);
            Name = "FormFFT";
            ((System.ComponentModel.ISupportInitialize)xyDiagram1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splineSeriesView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)series1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartControlFFT).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControlFFT;
    }
}
