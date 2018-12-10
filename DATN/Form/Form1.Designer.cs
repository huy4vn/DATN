namespace DATN
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.starDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightVectorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dATNDataSet = new DATN.DATNDataSet();
            this.weightVectorTableAdapter = new DATN.DATNDataSetTableAdapters.WeightVectorTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.rating = new System.Windows.Forms.TextBox();
            this.star = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rank = new System.Windows.Forms.TextBox();
            this.dATNDataSet1 = new DATN.DATNDataSet1();
            this.dataPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataPointTableAdapter = new DATN.DATNDataSet1TableAdapters.DataPointTableAdapter();
            this.tableAdapterManager = new DATN.DATNDataSet1TableAdapters.TableAdapterManager();
            this.dataPointDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.weightVectorBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightVectorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dATNDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dATNDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightVectorBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.starDataGridViewTextBoxColumn,
            this.ratingDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.weightVectorBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(9, 24);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(214, 269);
            this.dataGridView1.TabIndex = 0;
            // 
            // starDataGridViewTextBoxColumn
            // 
            this.starDataGridViewTextBoxColumn.DataPropertyName = "star";
            this.starDataGridViewTextBoxColumn.HeaderText = "star";
            this.starDataGridViewTextBoxColumn.Name = "starDataGridViewTextBoxColumn";
            // 
            // ratingDataGridViewTextBoxColumn
            // 
            this.ratingDataGridViewTextBoxColumn.DataPropertyName = "rating";
            this.ratingDataGridViewTextBoxColumn.HeaderText = "rating";
            this.ratingDataGridViewTextBoxColumn.Name = "ratingDataGridViewTextBoxColumn";
            // 
            // weightVectorBindingSource
            // 
            this.weightVectorBindingSource.DataMember = "WeightVector";
            this.weightVectorBindingSource.DataSource = this.dATNDataSet;
            // 
            // dATNDataSet
            // 
            this.dATNDataSet.DataSetName = "DATNDataSet";
            this.dATNDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // weightVectorTableAdapter
            // 
            this.weightVectorTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(506, 238);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 58);
            this.button1.TabIndex = 1;
            this.button1.Text = "Process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rating
            // 
            this.rating.Location = new System.Drawing.Point(546, 41);
            this.rating.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rating.Name = "rating";
            this.rating.Size = new System.Drawing.Size(76, 20);
            this.rating.TabIndex = 2;
            // 
            // star
            // 
            this.star.Location = new System.Drawing.Point(546, 76);
            this.star.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.star.Name = "star";
            this.star.Size = new System.Drawing.Size(76, 20);
            this.star.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(493, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data Point Entry";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(469, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rating:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(486, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Star:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(456, 112);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ranking:";
            // 
            // rank
            // 
            this.rank.Location = new System.Drawing.Point(546, 114);
            this.rank.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rank.Name = "rank";
            this.rank.Size = new System.Drawing.Size(76, 20);
            this.rank.TabIndex = 8;
            // 
            // dATNDataSet1
            // 
            this.dATNDataSet1.DataSetName = "DATNDataSet1";
            this.dATNDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataPointBindingSource
            // 
            this.dataPointBindingSource.DataMember = "DataPoint";
            this.dataPointBindingSource.DataSource = this.dATNDataSet1;
            // 
            // dataPointTableAdapter
            // 
            this.dataPointTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.DataPointTableAdapter = this.dataPointTableAdapter;
            this.tableAdapterManager.UpdateOrder = DATN.DATNDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // dataPointDataGridView
            // 
            this.dataPointDataGridView.AutoGenerateColumns = false;
            this.dataPointDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPointDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataPointDataGridView.DataSource = this.dataPointBindingSource;
            this.dataPointDataGridView.Location = new System.Drawing.Point(233, 24);
            this.dataPointDataGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataPointDataGridView.Name = "dataPointDataGridView";
            this.dataPointDataGridView.RowTemplate.Height = 24;
            this.dataPointDataGridView.Size = new System.Drawing.Size(218, 271);
            this.dataPointDataGridView.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "rating";
            this.dataGridViewTextBoxColumn2.HeaderText = "rating";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "star";
            this.dataGridViewTextBoxColumn3.HeaderText = "star";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(673, 24);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(364, 272);
            this.dataGridView2.TabIndex = 10;
            // 
            // weightVectorBindingSource1
            // 
            this.weightVectorBindingSource1.DataMember = "WeightVector";
            this.weightVectorBindingSource1.DataSource = this.dATNDataSet;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 338);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataPointDataGridView);
            this.Controls.Add(this.rank);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.star);
            this.Controls.Add(this.rating);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightVectorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dATNDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dATNDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightVectorBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DATNDataSet dATNDataSet;
        private System.Windows.Forms.BindingSource weightVectorBindingSource;
        private DATNDataSetTableAdapters.WeightVectorTableAdapter weightVectorTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn starDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratingDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox rating;
        private System.Windows.Forms.TextBox star;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rank;
        private DATNDataSet1 dATNDataSet1;
        private System.Windows.Forms.BindingSource dataPointBindingSource;
        private DATNDataSet1TableAdapters.DataPointTableAdapter dataPointTableAdapter;
        private DATNDataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView dataPointDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource weightVectorBindingSource1;
    }
}

