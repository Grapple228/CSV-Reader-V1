namespace CSVtoBD
{
    partial class Main_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LoadFile_Button = new System.Windows.Forms.Button();
            this.CurrentFile_Label = new System.Windows.Forms.Label();
            this.ClearFile_Button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.WriteFile_Button = new System.Windows.Forms.Button();
            this.FilePreview_GroupBox = new System.Windows.Forms.GroupBox();
            this.fixedColumnLabel = new System.Windows.Forms.Label();
            this.clearTableButton = new System.Windows.Forms.Button();
            this.isColumnFixed_CheckBox = new System.Windows.Forms.CheckBox();
            this.currentBDTable_Label = new System.Windows.Forms.Label();
            this.currentBDTable_ComboBox = new System.Windows.Forms.ComboBox();
            this.isDynamicUpdate_CheckBox = new System.Windows.Forms.CheckBox();
            this.separator_Label = new System.Windows.Forms.Label();
            this.separator_TextBox = new System.Windows.Forms.TextBox();
            this.DBpath_Label = new System.Windows.Forms.Label();
            this.chooseDB_Button = new System.Windows.Forms.Button();
            this.isCreateNewFile_CheckBox = new System.Windows.Forms.CheckBox();
            this.increaseColumnCount_Button = new System.Windows.Forms.Button();
            this.decreaseColumnCount_Button = new System.Windows.Forms.Button();
            this.columnCount_TextBox = new System.Windows.Forms.TextBox();
            this.columnCount_Label = new System.Windows.Forms.Label();
            this.isWriteIntoBD_CheckBox = new System.Windows.Forms.CheckBox();
            this.isWriteIntoFile_CheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshTable_Button = new System.Windows.Forms.Button();
            this.previewTable_DataGridView = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.isDeleteEmptyRows_checkBox = new System.Windows.Forms.CheckBox();
            this.autoSizeColumnsMode_CheckBox = new System.Windows.Forms.CheckBox();
            this.getInfoAboutDataGridView_button = new System.Windows.Forms.Button();
            this.tableForInserting_ComboBox = new System.Windows.Forms.ComboBox();
            this.TableForInserting_Label = new System.Windows.Forms.Label();
            this.FilePreview_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewTable_DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadFile_Button
            // 
            this.LoadFile_Button.Location = new System.Drawing.Point(12, 12);
            this.LoadFile_Button.Name = "LoadFile_Button";
            this.LoadFile_Button.Size = new System.Drawing.Size(103, 23);
            this.LoadFile_Button.TabIndex = 0;
            this.LoadFile_Button.Text = "Выбрать файл";
            this.toolTip1.SetToolTip(this.LoadFile_Button, "Выбор файла для считывания");
            this.LoadFile_Button.UseVisualStyleBackColor = true;
            this.LoadFile_Button.Click += new System.EventHandler(this.LoadFile_Button_Click);
            // 
            // CurrentFile_Label
            // 
            this.CurrentFile_Label.AutoSize = true;
            this.CurrentFile_Label.Location = new System.Drawing.Point(12, 38);
            this.CurrentFile_Label.Name = "CurrentFile_Label";
            this.CurrentFile_Label.Size = new System.Drawing.Size(92, 13);
            this.CurrentFile_Label.TabIndex = 1;
            this.CurrentFile_Label.Text = "Файл не выбран";
            this.toolTip1.SetToolTip(this.CurrentFile_Label, "Путь до выбранного файла");
            // 
            // ClearFile_Button
            // 
            this.ClearFile_Button.Location = new System.Drawing.Point(121, 12);
            this.ClearFile_Button.Name = "ClearFile_Button";
            this.ClearFile_Button.Size = new System.Drawing.Size(103, 23);
            this.ClearFile_Button.TabIndex = 2;
            this.ClearFile_Button.Text = "Очистить выбор";
            this.toolTip1.SetToolTip(this.ClearFile_Button, "Отменить выбор файла");
            this.ClearFile_Button.UseVisualStyleBackColor = true;
            this.ClearFile_Button.Visible = false;
            this.ClearFile_Button.Click += new System.EventHandler(this.ClearFile_Button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // WriteFile_Button
            // 
            this.WriteFile_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.WriteFile_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.WriteFile_Button.Location = new System.Drawing.Point(6, 495);
            this.WriteFile_Button.Name = "WriteFile_Button";
            this.WriteFile_Button.Size = new System.Drawing.Size(103, 42);
            this.WriteFile_Button.TabIndex = 4;
            this.WriteFile_Button.Text = "Записать";
            this.toolTip1.SetToolTip(this.WriteFile_Button, "Выполнить запись");
            this.WriteFile_Button.UseVisualStyleBackColor = true;
            this.WriteFile_Button.Click += new System.EventHandler(this.WriteFile_Button_Click);
            // 
            // FilePreview_GroupBox
            // 
            this.FilePreview_GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilePreview_GroupBox.Controls.Add(this.TableForInserting_Label);
            this.FilePreview_GroupBox.Controls.Add(this.tableForInserting_ComboBox);
            this.FilePreview_GroupBox.Controls.Add(this.fixedColumnLabel);
            this.FilePreview_GroupBox.Controls.Add(this.clearTableButton);
            this.FilePreview_GroupBox.Controls.Add(this.isColumnFixed_CheckBox);
            this.FilePreview_GroupBox.Controls.Add(this.currentBDTable_Label);
            this.FilePreview_GroupBox.Controls.Add(this.currentBDTable_ComboBox);
            this.FilePreview_GroupBox.Controls.Add(this.isDynamicUpdate_CheckBox);
            this.FilePreview_GroupBox.Controls.Add(this.separator_Label);
            this.FilePreview_GroupBox.Controls.Add(this.separator_TextBox);
            this.FilePreview_GroupBox.Controls.Add(this.DBpath_Label);
            this.FilePreview_GroupBox.Controls.Add(this.chooseDB_Button);
            this.FilePreview_GroupBox.Controls.Add(this.isCreateNewFile_CheckBox);
            this.FilePreview_GroupBox.Controls.Add(this.increaseColumnCount_Button);
            this.FilePreview_GroupBox.Controls.Add(this.decreaseColumnCount_Button);
            this.FilePreview_GroupBox.Controls.Add(this.columnCount_TextBox);
            this.FilePreview_GroupBox.Controls.Add(this.columnCount_Label);
            this.FilePreview_GroupBox.Controls.Add(this.isWriteIntoBD_CheckBox);
            this.FilePreview_GroupBox.Controls.Add(this.isWriteIntoFile_CheckBox);
            this.FilePreview_GroupBox.Controls.Add(this.RefreshTable_Button);
            this.FilePreview_GroupBox.Controls.Add(this.previewTable_DataGridView);
            this.FilePreview_GroupBox.Controls.Add(this.WriteFile_Button);
            this.FilePreview_GroupBox.Location = new System.Drawing.Point(15, 86);
            this.FilePreview_GroupBox.Name = "FilePreview_GroupBox";
            this.FilePreview_GroupBox.Size = new System.Drawing.Size(776, 542);
            this.FilePreview_GroupBox.TabIndex = 5;
            this.FilePreview_GroupBox.TabStop = false;
            this.FilePreview_GroupBox.Text = "Предпросмотр файла";
            this.FilePreview_GroupBox.Visible = false;
            // 
            // fixedColumnLabel
            // 
            this.fixedColumnLabel.AutoSize = true;
            this.fixedColumnLabel.Location = new System.Drawing.Point(200, 47);
            this.fixedColumnLabel.Name = "fixedColumnLabel";
            this.fixedColumnLabel.Size = new System.Drawing.Size(0, 13);
            this.fixedColumnLabel.TabIndex = 22;
            this.fixedColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.fixedColumnLabel, "Зафикированные столбцы");
            // 
            // clearTableButton
            // 
            this.clearTableButton.Location = new System.Drawing.Point(7, 46);
            this.clearTableButton.Name = "clearTableButton";
            this.clearTableButton.Size = new System.Drawing.Size(75, 23);
            this.clearTableButton.TabIndex = 21;
            this.clearTableButton.Text = "Очистить";
            this.toolTip1.SetToolTip(this.clearTableButton, "Очистка строк таблицы");
            this.clearTableButton.UseVisualStyleBackColor = true;
            this.clearTableButton.Click += new System.EventHandler(this.ClearTable_Button_Click);
            // 
            // isColumnFixed_CheckBox
            // 
            this.isColumnFixed_CheckBox.AutoSize = true;
            this.isColumnFixed_CheckBox.Checked = true;
            this.isColumnFixed_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isColumnFixed_CheckBox.Location = new System.Drawing.Point(109, 46);
            this.isColumnFixed_CheckBox.Name = "isColumnFixed_CheckBox";
            this.isColumnFixed_CheckBox.Size = new System.Drawing.Size(79, 17);
            this.isColumnFixed_CheckBox.TabIndex = 20;
            this.isColumnFixed_CheckBox.Text = "Фиксация";
            this.toolTip1.SetToolTip(this.isColumnFixed_CheckBox, "Выбор текущих столбцов как зафиксированных");
            this.isColumnFixed_CheckBox.UseVisualStyleBackColor = true;
            this.isColumnFixed_CheckBox.CheckedChanged += new System.EventHandler(this.IsColumnFixed_CheckBox_CheckedChanged);
            // 
            // currentBDTable_Label
            // 
            this.currentBDTable_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentBDTable_Label.AutoSize = true;
            this.currentBDTable_Label.Location = new System.Drawing.Point(584, 25);
            this.currentBDTable_Label.Name = "currentBDTable_Label";
            this.currentBDTable_Label.Size = new System.Drawing.Size(56, 13);
            this.currentBDTable_Label.TabIndex = 19;
            this.currentBDTable_Label.Text = "Таблица :";
            this.currentBDTable_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // currentBDTable_ComboBox
            // 
            this.currentBDTable_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentBDTable_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentBDTable_ComboBox.FormattingEnabled = true;
            this.currentBDTable_ComboBox.Location = new System.Drawing.Point(646, 20);
            this.currentBDTable_ComboBox.Name = "currentBDTable_ComboBox";
            this.currentBDTable_ComboBox.Size = new System.Drawing.Size(121, 21);
            this.currentBDTable_ComboBox.TabIndex = 18;
            this.toolTip1.SetToolTip(this.currentBDTable_ComboBox, "Таблица из БД");
            this.currentBDTable_ComboBox.SelectedIndexChanged += new System.EventHandler(this.CurrentBDTable_ComboBox_SelectedIndexChanged);
            // 
            // isDynamicUpdate_CheckBox
            // 
            this.isDynamicUpdate_CheckBox.AutoSize = true;
            this.isDynamicUpdate_CheckBox.Checked = true;
            this.isDynamicUpdate_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isDynamicUpdate_CheckBox.Location = new System.Drawing.Point(289, 46);
            this.isDynamicUpdate_CheckBox.Name = "isDynamicUpdate_CheckBox";
            this.isDynamicUpdate_CheckBox.Size = new System.Drawing.Size(127, 17);
            this.isDynamicUpdate_CheckBox.TabIndex = 17;
            this.isDynamicUpdate_CheckBox.Text = "Динам. обновление";
            this.toolTip1.SetToolTip(this.isDynamicUpdate_CheckBox, "Обновление таблицы при изменении разделителя");
            this.isDynamicUpdate_CheckBox.UseVisualStyleBackColor = true;
            this.isDynamicUpdate_CheckBox.CheckedChanged += new System.EventHandler(this.IsDynamicUpdate_CheckBox_CheckedChanged);
            // 
            // separator_Label
            // 
            this.separator_Label.AutoSize = true;
            this.separator_Label.Location = new System.Drawing.Point(286, 23);
            this.separator_Label.Name = "separator_Label";
            this.separator_Label.Size = new System.Drawing.Size(79, 13);
            this.separator_Label.TabIndex = 16;
            this.separator_Label.Text = "Разделитель :";
            this.separator_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // separator_TextBox
            // 
            this.separator_TextBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.separator_TextBox.Location = new System.Drawing.Point(371, 19);
            this.separator_TextBox.MaxLength = 1;
            this.separator_TextBox.Name = "separator_TextBox";
            this.separator_TextBox.Size = new System.Drawing.Size(24, 21);
            this.separator_TextBox.TabIndex = 15;
            this.separator_TextBox.Text = ";";
            this.separator_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.separator_TextBox.TextChanged += new System.EventHandler(this.Separator_TextBox_TextChanged);
            // 
            // DBpath_Label
            // 
            this.DBpath_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DBpath_Label.AutoSize = true;
            this.DBpath_Label.Location = new System.Drawing.Point(331, 500);
            this.DBpath_Label.Name = "DBpath_Label";
            this.DBpath_Label.Size = new System.Drawing.Size(85, 13);
            this.DBpath_Label.TabIndex = 14;
            this.DBpath_Label.Text = "БД не выбрана";
            this.toolTip1.SetToolTip(this.DBpath_Label, "Путь до выбранной БД");
            this.DBpath_Label.Visible = false;
            // 
            // chooseDB_Button
            // 
            this.chooseDB_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chooseDB_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseDB_Button.Location = new System.Drawing.Point(222, 495);
            this.chooseDB_Button.Name = "chooseDB_Button";
            this.chooseDB_Button.Size = new System.Drawing.Size(103, 23);
            this.chooseDB_Button.TabIndex = 13;
            this.chooseDB_Button.Text = "Выбрать БД (SQL)";
            this.toolTip1.SetToolTip(this.chooseDB_Button, "Указание пути до БД");
            this.chooseDB_Button.UseVisualStyleBackColor = true;
            this.chooseDB_Button.Visible = false;
            this.chooseDB_Button.Click += new System.EventHandler(this.ChooseDB_Button_Click);
            // 
            // isCreateNewFile_CheckBox
            // 
            this.isCreateNewFile_CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isCreateNewFile_CheckBox.AutoSize = true;
            this.isCreateNewFile_CheckBox.Location = new System.Drawing.Point(222, 520);
            this.isCreateNewFile_CheckBox.Name = "isCreateNewFile_CheckBox";
            this.isCreateNewFile_CheckBox.Size = new System.Drawing.Size(103, 17);
            this.isCreateNewFile_CheckBox.TabIndex = 12;
            this.isCreateNewFile_CheckBox.Text = "Создать новый";
            this.toolTip1.SetToolTip(this.isCreateNewFile_CheckBox, "Создание нового файла");
            this.isCreateNewFile_CheckBox.UseVisualStyleBackColor = true;
            // 
            // increaseColumnCount_Button
            // 
            this.increaseColumnCount_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.increaseColumnCount_Button.Location = new System.Drawing.Point(252, 19);
            this.increaseColumnCount_Button.Name = "increaseColumnCount_Button";
            this.increaseColumnCount_Button.Size = new System.Drawing.Size(11, 13);
            this.increaseColumnCount_Button.TabIndex = 11;
            this.increaseColumnCount_Button.TabStop = false;
            this.increaseColumnCount_Button.Text = "▲";
            this.increaseColumnCount_Button.UseVisualStyleBackColor = true;
            this.increaseColumnCount_Button.Click += new System.EventHandler(this.IncreaseColumnCount_Button_Click);
            // 
            // decreaseColumnCount_Button
            // 
            this.decreaseColumnCount_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decreaseColumnCount_Button.Location = new System.Drawing.Point(252, 31);
            this.decreaseColumnCount_Button.Name = "decreaseColumnCount_Button";
            this.decreaseColumnCount_Button.Size = new System.Drawing.Size(11, 13);
            this.decreaseColumnCount_Button.TabIndex = 11;
            this.decreaseColumnCount_Button.TabStop = false;
            this.decreaseColumnCount_Button.Text = "▼";
            this.decreaseColumnCount_Button.UseVisualStyleBackColor = true;
            this.decreaseColumnCount_Button.Click += new System.EventHandler(this.DecreaseColumnCount_Button_Click);
            // 
            // columnCount_TextBox
            // 
            this.columnCount_TextBox.Location = new System.Drawing.Point(203, 20);
            this.columnCount_TextBox.Name = "columnCount_TextBox";
            this.columnCount_TextBox.Size = new System.Drawing.Size(60, 20);
            this.columnCount_TextBox.TabIndex = 9;
            this.columnCount_TextBox.Text = "3";
            this.columnCount_TextBox.TextChanged += new System.EventHandler(this.ColumnCount_TextBox_TextChanged);
            // 
            // columnCount_Label
            // 
            this.columnCount_Label.AutoSize = true;
            this.columnCount_Label.Location = new System.Drawing.Point(106, 23);
            this.columnCount_Label.Name = "columnCount_Label";
            this.columnCount_Label.Size = new System.Drawing.Size(97, 13);
            this.columnCount_Label.TabIndex = 10;
            this.columnCount_Label.Text = "Кол-во столбцов :";
            this.columnCount_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // isWriteIntoBD_CheckBox
            // 
            this.isWriteIntoBD_CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isWriteIntoBD_CheckBox.AutoSize = true;
            this.isWriteIntoBD_CheckBox.Location = new System.Drawing.Point(115, 499);
            this.isWriteIntoBD_CheckBox.Name = "isWriteIntoBD_CheckBox";
            this.isWriteIntoBD_CheckBox.Size = new System.Drawing.Size(91, 17);
            this.isWriteIntoBD_CheckBox.TabIndex = 8;
            this.isWriteIntoBD_CheckBox.Text = "Запись в БД";
            this.toolTip1.SetToolTip(this.isWriteIntoBD_CheckBox, "Запись в БД");
            this.isWriteIntoBD_CheckBox.UseVisualStyleBackColor = true;
            this.isWriteIntoBD_CheckBox.CheckedChanged += new System.EventHandler(this.IsWriteIntoBD_CheckBox_CheckedChanged);
            // 
            // isWriteIntoFile_CheckBox
            // 
            this.isWriteIntoFile_CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isWriteIntoFile_CheckBox.AutoSize = true;
            this.isWriteIntoFile_CheckBox.Checked = true;
            this.isWriteIntoFile_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isWriteIntoFile_CheckBox.Location = new System.Drawing.Point(115, 520);
            this.isWriteIntoFile_CheckBox.Name = "isWriteIntoFile_CheckBox";
            this.isWriteIntoFile_CheckBox.Size = new System.Drawing.Size(101, 17);
            this.isWriteIntoFile_CheckBox.TabIndex = 7;
            this.isWriteIntoFile_CheckBox.Text = "Запись в файл";
            this.toolTip1.SetToolTip(this.isWriteIntoFile_CheckBox, "Запись в файл");
            this.isWriteIntoFile_CheckBox.UseVisualStyleBackColor = true;
            this.isWriteIntoFile_CheckBox.CheckedChanged += new System.EventHandler(this.IsWriteIntoFile_CheckBox_CheckedChanged);
            // 
            // RefreshTable_Button
            // 
            this.RefreshTable_Button.AccessibleDescription = "";
            this.RefreshTable_Button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RefreshTable_Button.Location = new System.Drawing.Point(7, 20);
            this.RefreshTable_Button.Name = "RefreshTable_Button";
            this.RefreshTable_Button.Size = new System.Drawing.Size(75, 23);
            this.RefreshTable_Button.TabIndex = 6;
            this.RefreshTable_Button.Text = "Обновить";
            this.toolTip1.SetToolTip(this.RefreshTable_Button, "Перезаполнение таблицы из файла");
            this.RefreshTable_Button.UseVisualStyleBackColor = false;
            this.RefreshTable_Button.Click += new System.EventHandler(this.RefreshTable_Button_Click);
            // 
            // previewTable_DataGridView
            // 
            this.previewTable_DataGridView.AllowUserToOrderColumns = true;
            this.previewTable_DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewTable_DataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.previewTable_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.previewTable_DataGridView.Location = new System.Drawing.Point(7, 75);
            this.previewTable_DataGridView.Name = "previewTable_DataGridView";
            this.previewTable_DataGridView.Size = new System.Drawing.Size(760, 405);
            this.previewTable_DataGridView.TabIndex = 5;
            this.previewTable_DataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PreviewTable_DataGridView_CellEndEdit);
            // 
            // isDeleteEmptyRows_checkBox
            // 
            this.isDeleteEmptyRows_checkBox.AutoSize = true;
            this.isDeleteEmptyRows_checkBox.Checked = true;
            this.isDeleteEmptyRows_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isDeleteEmptyRows_checkBox.Location = new System.Drawing.Point(21, 61);
            this.isDeleteEmptyRows_checkBox.Name = "isDeleteEmptyRows_checkBox";
            this.isDeleteEmptyRows_checkBox.Size = new System.Drawing.Size(141, 17);
            this.isDeleteEmptyRows_checkBox.TabIndex = 6;
            this.isDeleteEmptyRows_checkBox.Text = "Скрыть пустые строки";
            this.toolTip1.SetToolTip(this.isDeleteEmptyRows_checkBox, "Скрыть пустые строки");
            this.isDeleteEmptyRows_checkBox.UseVisualStyleBackColor = true;
            this.isDeleteEmptyRows_checkBox.CheckedChanged += new System.EventHandler(this.IsDeleteEmptyRows_checkBox_CheckedChanged);
            // 
            // autoSizeColumnsMode_CheckBox
            // 
            this.autoSizeColumnsMode_CheckBox.AutoSize = true;
            this.autoSizeColumnsMode_CheckBox.Checked = true;
            this.autoSizeColumnsMode_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSizeColumnsMode_CheckBox.Location = new System.Drawing.Point(173, 61);
            this.autoSizeColumnsMode_CheckBox.Name = "autoSizeColumnsMode_CheckBox";
            this.autoSizeColumnsMode_CheckBox.Size = new System.Drawing.Size(146, 17);
            this.autoSizeColumnsMode_CheckBox.TabIndex = 23;
            this.autoSizeColumnsMode_CheckBox.Text = "Растягивать по ширине";
            this.toolTip1.SetToolTip(this.autoSizeColumnsMode_CheckBox, "Растягивание столбцов по наиболее длинному значению");
            this.autoSizeColumnsMode_CheckBox.UseVisualStyleBackColor = true;
            this.autoSizeColumnsMode_CheckBox.CheckedChanged += new System.EventHandler(this.autoSizeColumnsMode_CheckBox_CheckedChanged);
            // 
            // getInfoAboutDataGridView_button
            // 
            this.getInfoAboutDataGridView_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getInfoAboutDataGridView_button.Location = new System.Drawing.Point(667, 12);
            this.getInfoAboutDataGridView_button.Name = "getInfoAboutDataGridView_button";
            this.getInfoAboutDataGridView_button.Size = new System.Drawing.Size(121, 23);
            this.getInfoAboutDataGridView_button.TabIndex = 23;
            this.getInfoAboutDataGridView_button.Text = "Информация";
            this.toolTip1.SetToolTip(this.getInfoAboutDataGridView_button, "Выводит информацию об таблице ниже");
            this.getInfoAboutDataGridView_button.UseVisualStyleBackColor = true;
            this.getInfoAboutDataGridView_button.Visible = false;
            this.getInfoAboutDataGridView_button.Click += new System.EventHandler(this.GetInfoAboutDataGridView_button_Click);
            // 
            // tableForInserting_ComboBox
            // 
            this.tableForInserting_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableForInserting_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableForInserting_ComboBox.FormattingEnabled = true;
            this.tableForInserting_ComboBox.Location = new System.Drawing.Point(646, 516);
            this.tableForInserting_ComboBox.Name = "tableForInserting_ComboBox";
            this.tableForInserting_ComboBox.Size = new System.Drawing.Size(121, 21);
            this.tableForInserting_ComboBox.TabIndex = 23;
            this.tableForInserting_ComboBox.Visible = false;
            // 
            // TableForInserting_Label
            // 
            this.TableForInserting_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableForInserting_Label.AutoSize = true;
            this.TableForInserting_Label.Location = new System.Drawing.Point(567, 519);
            this.TableForInserting_Label.Name = "TableForInserting_Label";
            this.TableForInserting_Label.Size = new System.Drawing.Size(73, 13);
            this.TableForInserting_Label.TabIndex = 24;
            this.TableForInserting_Label.Text = "Записать в : ";
            this.TableForInserting_Label.Visible = false;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 640);
            this.Controls.Add(this.getInfoAboutDataGridView_button);
            this.Controls.Add(this.autoSizeColumnsMode_CheckBox);
            this.Controls.Add(this.isDeleteEmptyRows_checkBox);
            this.Controls.Add(this.FilePreview_GroupBox);
            this.Controls.Add(this.ClearFile_Button);
            this.Controls.Add(this.CurrentFile_Label);
            this.Controls.Add(this.LoadFile_Button);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Main_Form";
            this.Text = "CSV редактор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FilePreview_GroupBox.ResumeLayout(false);
            this.FilePreview_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewTable_DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadFile_Button;
        private System.Windows.Forms.Label CurrentFile_Label;
        private System.Windows.Forms.Button ClearFile_Button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button WriteFile_Button;
        private System.Windows.Forms.GroupBox FilePreview_GroupBox;
        private System.Windows.Forms.DataGridView previewTable_DataGridView;
        private System.Windows.Forms.Button RefreshTable_Button;
        private System.Windows.Forms.CheckBox isWriteIntoBD_CheckBox;
        private System.Windows.Forms.CheckBox isWriteIntoFile_CheckBox;
        private System.Windows.Forms.TextBox columnCount_TextBox;
        private System.Windows.Forms.Label columnCount_Label;
        private System.Windows.Forms.Button increaseColumnCount_Button;
        private System.Windows.Forms.Button decreaseColumnCount_Button;
        private System.Windows.Forms.CheckBox isCreateNewFile_CheckBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button chooseDB_Button;
        private System.Windows.Forms.Label DBpath_Label;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label separator_Label;
        private System.Windows.Forms.TextBox separator_TextBox;
        private System.Windows.Forms.CheckBox isDynamicUpdate_CheckBox;
        private System.Windows.Forms.ComboBox currentBDTable_ComboBox;
        private System.Windows.Forms.Label currentBDTable_Label;
        private System.Windows.Forms.CheckBox isColumnFixed_CheckBox;
        private System.Windows.Forms.Button clearTableButton;
        private System.Windows.Forms.Label fixedColumnLabel;
        private System.Windows.Forms.CheckBox isDeleteEmptyRows_checkBox;
        private System.Windows.Forms.CheckBox autoSizeColumnsMode_CheckBox;
        private System.Windows.Forms.Button getInfoAboutDataGridView_button;
        private System.Windows.Forms.Label TableForInserting_Label;
        private System.Windows.Forms.ComboBox tableForInserting_ComboBox;
    }
}

