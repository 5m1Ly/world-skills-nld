namespace Yardkeeper
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lblYardName = new System.Windows.Forms.Label();
            this.flpWrapper = new System.Windows.Forms.FlowLayoutPanel();
            this.flpLeftContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpMiniMap = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMiniMap = new System.Windows.Forms.Panel();
            this.yrdMiniMap = new Yardkeeper.Yard();
            this.flpRightContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpCurrentConditions = new System.Windows.Forms.TableLayoutPanel();
            this.tlpConditionsTitle = new System.Windows.Forms.TableLayoutPanel();
            this.lblConditionsTitle = new System.Windows.Forms.Label();
            this.clkTimeOfDay = new Yardkeeper.Interface.Clock();
            this.pnlConditionsTelemetry = new System.Windows.Forms.Panel();
            this.lblDewpointCaption = new System.Windows.Forms.Label();
            this.lblPressureCaption = new System.Windows.Forms.Label();
            this.lblHumidityCaption = new System.Windows.Forms.Label();
            this.lblTemperatureCaption = new System.Windows.Forms.Label();
            this.pbForecast = new System.Windows.Forms.PictureBox();
            this.lblDewpointValue = new System.Windows.Forms.Label();
            this.lblPressureValue = new System.Windows.Forms.Label();
            this.lblHumidityValue = new System.Windows.Forms.Label();
            this.lblTemperatureValue = new System.Windows.Forms.Label();
            this.pbDewpoint = new System.Windows.Forms.PictureBox();
            this.pbPressure = new System.Windows.Forms.PictureBox();
            this.pbTemperature = new System.Windows.Forms.PictureBox();
            this.pbHumidity = new System.Windows.Forms.PictureBox();
            this.tlpCurrentSchedule = new System.Windows.Forms.TableLayoutPanel();
            this.tlpScheduleTitle = new System.Windows.Forms.TableLayoutPanel();
            this.lblScheduleTitle = new System.Windows.Forms.Label();
            this.pnlScheduleManager = new System.Windows.Forms.Panel();
            this.flpWrapper.SuspendLayout();
            this.flpLeftContainer.SuspendLayout();
            this.tlpMiniMap.SuspendLayout();
            this.pnlMiniMap.SuspendLayout();
            this.flpRightContainer.SuspendLayout();
            this.tlpCurrentConditions.SuspendLayout();
            this.tlpConditionsTitle.SuspendLayout();
            this.pnlConditionsTelemetry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbForecast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDewpoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPressure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHumidity)).BeginInit();
            this.tlpCurrentSchedule.SuspendLayout();
            this.tlpScheduleTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblYardName
            // 
            this.lblYardName.AutoSize = true;
            this.lblYardName.Font = new System.Drawing.Font("Calibri", 22F);
            this.lblYardName.Location = new System.Drawing.Point(0, 0);
            this.lblYardName.Margin = new System.Windows.Forms.Padding(0);
            this.lblYardName.Name = "lblYardName";
            this.lblYardName.Size = new System.Drawing.Size(175, 37);
            this.lblYardName.TabIndex = 1;
            this.lblYardName.Text = "My Backyard";
            // 
            // flpWrapper
            // 
            this.flpWrapper.Controls.Add(this.flpLeftContainer);
            this.flpWrapper.Controls.Add(this.flpRightContainer);
            this.flpWrapper.Location = new System.Drawing.Point(0, 0);
            this.flpWrapper.Margin = new System.Windows.Forms.Padding(0);
            this.flpWrapper.Name = "flpWrapper";
            this.flpWrapper.Padding = new System.Windows.Forms.Padding(20, 35, 20, 20);
            this.flpWrapper.Size = new System.Drawing.Size(1024, 620);
            this.flpWrapper.TabIndex = 4;
            // 
            // flpLeftContainer
            // 
            this.flpLeftContainer.Controls.Add(this.tlpMiniMap);
            this.flpLeftContainer.Location = new System.Drawing.Point(20, 35);
            this.flpLeftContainer.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.flpLeftContainer.Name = "flpLeftContainer";
            this.flpLeftContainer.Size = new System.Drawing.Size(373, 559);
            this.flpLeftContainer.TabIndex = 0;
            // 
            // tlpMiniMap
            // 
            this.tlpMiniMap.ColumnCount = 1;
            this.tlpMiniMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiniMap.Controls.Add(this.lblYardName, 0, 0);
            this.tlpMiniMap.Controls.Add(this.pnlMiniMap, 0, 1);
            this.tlpMiniMap.Location = new System.Drawing.Point(0, 0);
            this.tlpMiniMap.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMiniMap.Name = "tlpMiniMap";
            this.tlpMiniMap.RowCount = 2;
            this.tlpMiniMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMiniMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiniMap.Size = new System.Drawing.Size(373, 559);
            this.tlpMiniMap.TabIndex = 0;
            // 
            // pnlMiniMap
            // 
            this.pnlMiniMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.pnlMiniMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMiniMap.Controls.Add(this.yrdMiniMap);
            this.pnlMiniMap.Location = new System.Drawing.Point(0, 50);
            this.pnlMiniMap.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMiniMap.Name = "pnlMiniMap";
            this.pnlMiniMap.Size = new System.Drawing.Size(373, 509);
            this.pnlMiniMap.TabIndex = 2;
            // 
            // yrdMiniMap
            // 
            this.yrdMiniMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.yrdMiniMap.BorderColor = System.Drawing.Color.Green;
            this.yrdMiniMap.BorderWidth = ((byte)(5));
            this.yrdMiniMap.Location = new System.Drawing.Point(-1, -1);
            this.yrdMiniMap.Name = "yrdMiniMap";
            this.yrdMiniMap.Size = new System.Drawing.Size(362, 465);
            this.yrdMiniMap.TabIndex = 0;
            this.yrdMiniMap.Text = "yard1";
            // 
            // flpRightContainer
            // 
            this.flpRightContainer.Controls.Add(this.tlpCurrentConditions);
            this.flpRightContainer.Controls.Add(this.tlpCurrentSchedule);
            this.flpRightContainer.Location = new System.Drawing.Point(416, 38);
            this.flpRightContainer.Name = "flpRightContainer";
            this.flpRightContainer.Size = new System.Drawing.Size(585, 556);
            this.flpRightContainer.TabIndex = 0;
            // 
            // tlpCurrentConditions
            // 
            this.tlpCurrentConditions.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tlpCurrentConditions.ColumnCount = 1;
            this.tlpCurrentConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCurrentConditions.Controls.Add(this.tlpConditionsTitle, 0, 0);
            this.tlpCurrentConditions.Controls.Add(this.pnlConditionsTelemetry, 0, 1);
            this.tlpCurrentConditions.Location = new System.Drawing.Point(0, 0);
            this.tlpCurrentConditions.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.tlpCurrentConditions.Name = "tlpCurrentConditions";
            this.tlpCurrentConditions.RowCount = 3;
            this.tlpCurrentConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpCurrentConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCurrentConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCurrentConditions.Size = new System.Drawing.Size(585, 237);
            this.tlpCurrentConditions.TabIndex = 5;
            // 
            // tlpConditionsTitle
            // 
            this.tlpConditionsTitle.ColumnCount = 2;
            this.tlpConditionsTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 349F));
            this.tlpConditionsTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpConditionsTitle.Controls.Add(this.lblConditionsTitle, 0, 0);
            this.tlpConditionsTitle.Controls.Add(this.clkTimeOfDay, 1, 0);
            this.tlpConditionsTitle.Location = new System.Drawing.Point(0, 0);
            this.tlpConditionsTitle.Margin = new System.Windows.Forms.Padding(0);
            this.tlpConditionsTitle.Name = "tlpConditionsTitle";
            this.tlpConditionsTitle.RowCount = 1;
            this.tlpConditionsTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpConditionsTitle.Size = new System.Drawing.Size(585, 36);
            this.tlpConditionsTitle.TabIndex = 4;
            // 
            // lblConditionsTitle
            // 
            this.lblConditionsTitle.AutoSize = true;
            this.lblConditionsTitle.Font = new System.Drawing.Font("Calibri", 22F);
            this.lblConditionsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblConditionsTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblConditionsTitle.Name = "lblConditionsTitle";
            this.lblConditionsTitle.Size = new System.Drawing.Size(249, 36);
            this.lblConditionsTitle.TabIndex = 3;
            this.lblConditionsTitle.Text = "Current Conditions";
            // 
            // clkTimeOfDay
            // 
            this.clkTimeOfDay.BackColor = System.Drawing.SystemColors.Control;
            this.clkTimeOfDay.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clkTimeOfDay.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.clkTimeOfDay.Format = "dd-mm-yyyy | HH:MM";
            this.clkTimeOfDay.Location = new System.Drawing.Point(349, 0);
            this.clkTimeOfDay.Margin = new System.Windows.Forms.Padding(0);
            this.clkTimeOfDay.Name = "clkTimeOfDay";
            this.clkTimeOfDay.RefreshRate = 60;
            this.clkTimeOfDay.Size = new System.Drawing.Size(235, 36);
            this.clkTimeOfDay.TabIndex = 2;
            this.clkTimeOfDay.Text = "01-02-2016 | 06:33";
            // 
            // pnlConditionsTelemetry
            // 
            this.pnlConditionsTelemetry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConditionsTelemetry.Controls.Add(this.lblDewpointCaption);
            this.pnlConditionsTelemetry.Controls.Add(this.lblPressureCaption);
            this.pnlConditionsTelemetry.Controls.Add(this.lblHumidityCaption);
            this.pnlConditionsTelemetry.Controls.Add(this.lblTemperatureCaption);
            this.pnlConditionsTelemetry.Controls.Add(this.pbForecast);
            this.pnlConditionsTelemetry.Controls.Add(this.lblDewpointValue);
            this.pnlConditionsTelemetry.Controls.Add(this.lblPressureValue);
            this.pnlConditionsTelemetry.Controls.Add(this.lblHumidityValue);
            this.pnlConditionsTelemetry.Controls.Add(this.lblTemperatureValue);
            this.pnlConditionsTelemetry.Controls.Add(this.pbDewpoint);
            this.pnlConditionsTelemetry.Controls.Add(this.pbPressure);
            this.pnlConditionsTelemetry.Controls.Add(this.pbTemperature);
            this.pnlConditionsTelemetry.Controls.Add(this.pbHumidity);
            this.pnlConditionsTelemetry.Location = new System.Drawing.Point(0, 50);
            this.pnlConditionsTelemetry.Margin = new System.Windows.Forms.Padding(0);
            this.pnlConditionsTelemetry.Name = "pnlConditionsTelemetry";
            this.pnlConditionsTelemetry.Size = new System.Drawing.Size(585, 167);
            this.pnlConditionsTelemetry.TabIndex = 5;
            // 
            // lblDewpointCaption
            // 
            this.lblDewpointCaption.AutoSize = true;
            this.lblDewpointCaption.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDewpointCaption.Location = new System.Drawing.Point(279, 35);
            this.lblDewpointCaption.Name = "lblDewpointCaption";
            this.lblDewpointCaption.Size = new System.Drawing.Size(64, 15);
            this.lblDewpointCaption.TabIndex = 3;
            this.lblDewpointCaption.Text = "Dew Point";
            // 
            // lblPressureCaption
            // 
            this.lblPressureCaption.AutoSize = true;
            this.lblPressureCaption.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPressureCaption.Location = new System.Drawing.Point(279, 105);
            this.lblPressureCaption.Name = "lblPressureCaption";
            this.lblPressureCaption.Size = new System.Drawing.Size(128, 15);
            this.lblPressureCaption.TabIndex = 3;
            this.lblPressureCaption.Text = "Atmospheric Pressure";
            // 
            // lblHumidityCaption
            // 
            this.lblHumidityCaption.AutoSize = true;
            this.lblHumidityCaption.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHumidityCaption.Location = new System.Drawing.Point(75, 105);
            this.lblHumidityCaption.Name = "lblHumidityCaption";
            this.lblHumidityCaption.Size = new System.Drawing.Size(57, 15);
            this.lblHumidityCaption.TabIndex = 3;
            this.lblHumidityCaption.Text = "Humidity";
            // 
            // lblTemperatureCaption
            // 
            this.lblTemperatureCaption.AutoSize = true;
            this.lblTemperatureCaption.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemperatureCaption.Location = new System.Drawing.Point(76, 35);
            this.lblTemperatureCaption.Name = "lblTemperatureCaption";
            this.lblTemperatureCaption.Size = new System.Drawing.Size(79, 15);
            this.lblTemperatureCaption.TabIndex = 3;
            this.lblTemperatureCaption.Text = "Temperature";
            // 
            // pbForecast
            // 
            this.pbForecast.Image = ((System.Drawing.Image)(resources.GetObject("pbForecast.Image")));
            this.pbForecast.Location = new System.Drawing.Point(434, 16);
            this.pbForecast.Name = "pbForecast";
            this.pbForecast.Size = new System.Drawing.Size(110, 110);
            this.pbForecast.TabIndex = 2;
            this.pbForecast.TabStop = false;
            // 
            // lblDewpointValue
            // 
            this.lblDewpointValue.AutoSize = true;
            this.lblDewpointValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDewpointValue.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDewpointValue.Location = new System.Drawing.Point(281, 59);
            this.lblDewpointValue.Name = "lblDewpointValue";
            this.lblDewpointValue.Size = new System.Drawing.Size(81, 29);
            this.lblDewpointValue.TabIndex = 1;
            this.lblDewpointValue.Text = "25.2 °C";
            // 
            // lblPressureValue
            // 
            this.lblPressureValue.AutoSize = true;
            this.lblPressureValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPressureValue.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPressureValue.Location = new System.Drawing.Point(281, 128);
            this.lblPressureValue.Name = "lblPressureValue";
            this.lblPressureValue.Size = new System.Drawing.Size(120, 29);
            this.lblPressureValue.TabIndex = 1;
            this.lblPressureValue.Text = "1024 mbar";
            // 
            // lblHumidityValue
            // 
            this.lblHumidityValue.AutoSize = true;
            this.lblHumidityValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblHumidityValue.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHumidityValue.Location = new System.Drawing.Point(78, 128);
            this.lblHumidityValue.Name = "lblHumidityValue";
            this.lblHumidityValue.Size = new System.Drawing.Size(55, 29);
            this.lblHumidityValue.TabIndex = 1;
            this.lblHumidityValue.Text = "54%";
            // 
            // lblTemperatureValue
            // 
            this.lblTemperatureValue.AutoSize = true;
            this.lblTemperatureValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTemperatureValue.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemperatureValue.Location = new System.Drawing.Point(78, 59);
            this.lblTemperatureValue.Name = "lblTemperatureValue";
            this.lblTemperatureValue.Size = new System.Drawing.Size(81, 29);
            this.lblTemperatureValue.TabIndex = 1;
            this.lblTemperatureValue.Text = "25.2 °C";
            // 
            // pbDewpoint
            // 
            this.pbDewpoint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbDewpoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbDewpoint.Image = global::Yardkeeper.Properties.Resources.telemetry_temperature;
            this.pbDewpoint.Location = new System.Drawing.Point(204, 16);
            this.pbDewpoint.Margin = new System.Windows.Forms.Padding(0);
            this.pbDewpoint.Name = "pbDewpoint";
            this.pbDewpoint.Size = new System.Drawing.Size(75, 75);
            this.pbDewpoint.TabIndex = 0;
            this.pbDewpoint.TabStop = false;
            // 
            // pbPressure
            // 
            this.pbPressure.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbPressure.Image = global::Yardkeeper.Properties.Resources.telemetry_barometer;
            this.pbPressure.Location = new System.Drawing.Point(204, 85);
            this.pbPressure.Margin = new System.Windows.Forms.Padding(0);
            this.pbPressure.Name = "pbPressure";
            this.pbPressure.Size = new System.Drawing.Size(75, 75);
            this.pbPressure.TabIndex = 0;
            this.pbPressure.TabStop = false;
            // 
            // pbTemperature
            // 
            this.pbTemperature.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbTemperature.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbTemperature.Image = global::Yardkeeper.Properties.Resources.telemetry_temperature;
            this.pbTemperature.Location = new System.Drawing.Point(1, 16);
            this.pbTemperature.Margin = new System.Windows.Forms.Padding(0);
            this.pbTemperature.Name = "pbTemperature";
            this.pbTemperature.Size = new System.Drawing.Size(75, 75);
            this.pbTemperature.TabIndex = 0;
            this.pbTemperature.TabStop = false;
            // 
            // pbHumidity
            // 
            this.pbHumidity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbHumidity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbHumidity.Image = global::Yardkeeper.Properties.Resources.telemetry_humidity;
            this.pbHumidity.Location = new System.Drawing.Point(1, 85);
            this.pbHumidity.Margin = new System.Windows.Forms.Padding(0);
            this.pbHumidity.Name = "pbHumidity";
            this.pbHumidity.Size = new System.Drawing.Size(75, 75);
            this.pbHumidity.TabIndex = 0;
            this.pbHumidity.TabStop = false;
            // 
            // tlpCurrentSchedule
            // 
            this.tlpCurrentSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tlpCurrentSchedule.ColumnCount = 1;
            this.tlpCurrentSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCurrentSchedule.Controls.Add(this.tlpScheduleTitle, 0, 0);
            this.tlpCurrentSchedule.Controls.Add(this.pnlScheduleManager, 0, 1);
            this.tlpCurrentSchedule.Location = new System.Drawing.Point(0, 257);
            this.tlpCurrentSchedule.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.tlpCurrentSchedule.Name = "tlpCurrentSchedule";
            this.tlpCurrentSchedule.RowCount = 2;
            this.tlpCurrentSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpCurrentSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCurrentSchedule.Size = new System.Drawing.Size(585, 299);
            this.tlpCurrentSchedule.TabIndex = 6;
            // 
            // tlpScheduleTitle
            // 
            this.tlpScheduleTitle.ColumnCount = 2;
            this.tlpScheduleTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 349F));
            this.tlpScheduleTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpScheduleTitle.Controls.Add(this.lblScheduleTitle, 0, 0);
            this.tlpScheduleTitle.Location = new System.Drawing.Point(0, 0);
            this.tlpScheduleTitle.Margin = new System.Windows.Forms.Padding(0);
            this.tlpScheduleTitle.Name = "tlpScheduleTitle";
            this.tlpScheduleTitle.RowCount = 1;
            this.tlpScheduleTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpScheduleTitle.Size = new System.Drawing.Size(585, 36);
            this.tlpScheduleTitle.TabIndex = 4;
            // 
            // lblScheduleTitle
            // 
            this.lblScheduleTitle.AutoSize = true;
            this.lblScheduleTitle.Font = new System.Drawing.Font("Calibri", 22F);
            this.lblScheduleTitle.Location = new System.Drawing.Point(0, 0);
            this.lblScheduleTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblScheduleTitle.Name = "lblScheduleTitle";
            this.lblScheduleTitle.Size = new System.Drawing.Size(229, 36);
            this.lblScheduleTitle.TabIndex = 3;
            this.lblScheduleTitle.Text = "Current Schedule";
            // 
            // pnlScheduleManager
            // 
            this.pnlScheduleManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlScheduleManager.Location = new System.Drawing.Point(0, 50);
            this.pnlScheduleManager.Margin = new System.Windows.Forms.Padding(0);
            this.pnlScheduleManager.Name = "pnlScheduleManager";
            this.pnlScheduleManager.Padding = new System.Windows.Forms.Padding(10);
            this.pnlScheduleManager.Size = new System.Drawing.Size(585, 249);
            this.pnlScheduleManager.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1024, 620);
            this.Controls.Add(this.flpWrapper);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Flow Software Solutions | Yardkeeper";
            this.flpWrapper.ResumeLayout(false);
            this.flpLeftContainer.ResumeLayout(false);
            this.tlpMiniMap.ResumeLayout(false);
            this.tlpMiniMap.PerformLayout();
            this.pnlMiniMap.ResumeLayout(false);
            this.flpRightContainer.ResumeLayout(false);
            this.tlpCurrentConditions.ResumeLayout(false);
            this.tlpConditionsTitle.ResumeLayout(false);
            this.tlpConditionsTitle.PerformLayout();
            this.pnlConditionsTelemetry.ResumeLayout(false);
            this.pnlConditionsTelemetry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbForecast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDewpoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPressure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHumidity)).EndInit();
            this.tlpCurrentSchedule.ResumeLayout(false);
            this.tlpScheduleTitle.ResumeLayout(false);
            this.tlpScheduleTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected Yard yrdMiniMap;
        protected System.Windows.Forms.Label lblYardName;
        protected System.Windows.Forms.FlowLayoutPanel flpWrapper;
        protected System.Windows.Forms.FlowLayoutPanel flpLeftContainer;
        protected System.Windows.Forms.FlowLayoutPanel flpRightContainer;
        protected System.Windows.Forms.Label lblConditionsTitle;
        protected Interface.Clock clkTimeOfDay;
        protected System.Windows.Forms.TableLayoutPanel tlpConditionsTitle;
        protected System.Windows.Forms.TableLayoutPanel tlpCurrentConditions;
        protected System.Windows.Forms.PictureBox pbTemperature;
        protected System.Windows.Forms.PictureBox pbDewpoint;
        protected System.Windows.Forms.PictureBox pbHumidity;
        protected System.Windows.Forms.PictureBox pbPressure;
        protected System.Windows.Forms.Panel pnlConditionsTelemetry;
        protected System.Windows.Forms.Label lblDewpointValue;
        protected System.Windows.Forms.Label lblPressureValue;
        protected System.Windows.Forms.Label lblHumidityValue;
        protected System.Windows.Forms.Label lblTemperatureValue;
        protected System.Windows.Forms.PictureBox pbForecast;
        protected System.Windows.Forms.TableLayoutPanel tlpMiniMap;
        protected System.Windows.Forms.Panel pnlMiniMap;
        protected System.Windows.Forms.TableLayoutPanel tlpCurrentSchedule;
        protected System.Windows.Forms.TableLayoutPanel tlpScheduleTitle;
        protected System.Windows.Forms.Label lblScheduleTitle;
        protected System.Windows.Forms.Panel pnlScheduleManager;
        private System.Windows.Forms.Label lblDewpointCaption;
        private System.Windows.Forms.Label lblTemperatureCaption;
        private System.Windows.Forms.Label lblHumidityCaption;
        private System.Windows.Forms.Label lblPressureCaption;
    }
}

