using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CSVtoBD
{
    // TODO:
    // ЗАПИСЬ В БД
    // Проверить ColumnCountInt с кол-вом колонок в текущей Таблице
    // Если isBD То при добавлении в БД удалить первые ColumnCountInt Значений и записать в таблицу
    // 
    // ЗАГРУЗИТЬ ФАЙЛ - ДОБАВИТЬ ВОЗМОЖНОСТЬ СОЗДАТЬ ТАБЛИЦУ, ЕСЛИ В БД ОНИ ОТСУТСТВУЮТ
    // В меню с таблицами добавить "ТАБЛИЦЫ" и "ДЕЙСТВИЯ", при нажатии на них должно перебрасывать на текущую выбранную таблицу, поставить их перед всеми проверками

    /// <summary>
    /// Основная программа
    /// </summary>
    public partial class Main_Form : Form
    {
        // ЛОГИЧЕСКИЕ
        /// <summary>
        /// Файл выбран
        /// </summary>
        private bool isFileLoaded = false;
        /// <summary>
        /// Были внесены изменения
        /// </summary>
        private bool isChanged = false;
        /// <summary>
        /// Выбранный файл - База данных
        /// </summary>
        private bool isBD;
        /// <summary>
        /// База данных была переоткрыта
        /// </summary>
        private bool isBDWasOpened = false;
        /// <summary>
        /// Для обработки comboBox
        /// </summary>
        private bool handleSelection = true;
        /// <summary>
        /// Временное условие фиксации
        /// </summary>
        private bool tempIsColumnFixed = false;
        
        // СПИСКИ
        /// <summary>
        /// Данные таблицы
        /// </summary>
        private List<string> datas = new List<string>();

        // СТРОКИ
        /// <summary>
        /// Путь до выбранного файла
        /// </summary>
        private string filePath = null;
        /// <summary>
        /// Путь до файла БД
        /// </summary>
        private string DBpath = null;
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        private string DBConnectionString = null;
        /// <summary>
        /// Сохранение предыдущего имени таблицы
        /// </summary>
        private string currentSelectedTable;

        // СИМВОЛЫ
        /// <summary>
        /// Выбранный разделитель
        /// </summary>
        private char curSeparator = ';';

        //ЦЕЛОЕ
        /// <summary>
        /// Количество столбцов 
        /// </summary>
        private int columnCount;

        /// <summary>
        /// Количество зафиксированных столбцов
        /// </summary>
        int fixedColumnCount = 0;

        // ИНИЦИАЛИЗАЦИЯ СТРУКТУР И КЛАССОВ
        /// <summary>
        /// Класс с методами
        /// </summary>
        readonly private Methods methods = new Methods();
        /// <summary>
        /// Параметры для создания новой таблицы
        /// </summary>
        Methods.TableParameters TableParameters;

        /// <summary>
        /// Загрузка формы
        /// </summary>
        public Main_Form()
        {
            InitializeComponent();

            previewTable_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            columnCount = 3;
            fixedColumnCount = 3;
            fixedColumnLabel.Text = fixedColumnCount.ToString();
        }

        // BUTTONs

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить файл"
        /// </summary>
        private void ClearFile_Button_Click(object sender, EventArgs e)
        {
            if (isChanged)
            {
                string message = "После выполнения данной операции все внесенные изменения будут утеряны.\nВы уверены?";
                var result = methods.CreateWarningMessage(message);
                if (result != DialogResult.Yes) return;
            }

            if (!isFileLoaded) return;

            isFileLoaded = false;
            isChanged = false;
            filePath = null;
            CurrentFile_Label.Text = "Файл не выбран";
            ChangeFormVisibility();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Записать"
        /// </summary>
        private void WriteFile_Button_Click(object sender, EventArgs e)
        {
            string message = "";

            if (isWriteIntoBD_CheckBox.Checked) // Если запись в БД
            {
                message += "Будет произведено добавление записей в БД\n";

                if (!isWriteIntoFile_CheckBox.Checked) // Если запись только в БД
                {
                    var answer = methods.CreateWarningMessage(message);
                    if (answer != DialogResult.Yes) return;

                    bdWriteMethod();
                    isChanged = false;
                    return;
                }

                var bdCreateNewFileAnswer = methods.CreateWarningMessage(message);
                if (bdCreateNewFileAnswer != DialogResult.Yes) return;

                bdWriteMethod();
                fileWriteMethod();
                isChanged = false;
                return;
            }

            if (isWriteIntoFile_CheckBox.Checked) // Если запись только в файл
            {
                if (!isCreateNewFile_CheckBox.Checked && !isBD) // Если перезапись файла
                {
                    message += $"Будет выполнена перезапись файла\nВы уверены, что хотите продолжить?";
                    var bdRewriteFileAnswer = methods.CreateWarningMessage(message);
                    if (bdRewriteFileAnswer != DialogResult.Yes) return;
                }

                fileWriteMethod();
                isChanged = false;
                return;
            }

            message += "Вы не выбрали выполняемую операцию!";
            MessageBox.Show(
                    message,
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

            // ПРОЦЕДУРЫ ДЛЯ ДОБАВЛЕНИЯ ДАННЫХ

            // Определение метода записи файла
            void fileWriteMethod()
            {
                // Если текущий файл - БАЗА ДАННЫХ
                if (isBD)
                {
                    // Получить путь для создания нового файла
                    string newFilePath = methods.GetNewFilePath(this);
                    if (newFilePath == "") return;

                    datas = methods.GetDataFromDataGridView(previewTable_DataGridView, columnCount);
                    methods.WriteDataToTextFile(newFilePath, datas, columnCount, curSeparator);

                    if (isColumnFixed_CheckBox.Checked) fixedColumnCount = columnCount;

                    methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                    return;
                }

                // Если текущий файл - ТЕКСТОВЫЙ
                if (isCreateNewFile_CheckBox.Checked) // Если создание НОВОГО файла
                {
                    // Получить путь для создания нового файла
                    string newFilePath = methods.GetNewFilePath(this);

                    if (newFilePath == "") return;

                    filePath = newFilePath;
                    CurrentFile_Label.Text = filePath;
                    isCreateNewFile_CheckBox.Checked = false;
                }

                datas = methods.GetDataFromDataGridView(previewTable_DataGridView, columnCount);
                methods.WriteDataToTextFile(filePath, datas, columnCount, curSeparator);

                if (isColumnFixed_CheckBox.Checked) fixedColumnCount = columnCount;

                methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
            }

            // Определение метода записи БД
            void bdWriteMethod()
            {
                // ЕСЛИ НЕ УКАЗАНА БД, ТО ТРЕБОВАНИЕ УКАЗАТЬ
                if (DBConnectionString == null || DBpath == null)
                {
                    var bdChooseAnswer = methods.CreateWarningMessage("Не указана БД, желаете указать?");
                    if (bdChooseAnswer != DialogResult.Yes) return;

                    var path = methods.GetSQLDBpath(this);
                    if (path == null) return;

                    string connectionStringForTest = methods.GetSQLDBconnectionString(path);

                    var result = methods.DBConnectionCheck(connectionStringForTest);

                    if (result == false)
                    {
                        MessageBox.Show("Не удалось выполнить подключение к БД!", "Возникла ошибка!");
                        DBpath = null;
                        DBConnectionString = null;
                        DBpath_Label.Text = "БД не выбрана";
                        return;
                    }

                    DBConnectionString = connectionStringForTest;
                    DBpath = path;
                    DBpath_Label.Text = DBpath;
                }

                string table = tableForInserting_ComboBox.Text;
                if(table == null || table.Replace(" ","") == "")
                {
                    MessageBox.Show("Таблица не выбрана!", "Возникла ошибка!");
                    return;
                }
                if (table == null) return;

                List<string> datasWithoutHeaders = new List<string>();
                for (int i = 0; i<datas.Count; i++)
                {
                    if (!(i < columnCount)) datasWithoutHeaders.Add(datas[i]);
                }
                
                // Запись в бд
                methods.WriteDataToDB(DBConnectionString, previewTable_DataGridView, datasWithoutHeaders, DBpath, table);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Увеличение столбца"
        /// </summary>
        private void IncreaseColumnCount_Button_Click(object sender, EventArgs e)
        {
            columnCount_TextBox.Text = (columnCount + 1).ToString();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Уменьшение столбца"
        /// </summary>
        private void DecreaseColumnCount_Button_Click(object sender, EventArgs e)
        {
            if (columnCount <= 1) return;

            columnCount_TextBox.Text = (columnCount - 1).ToString();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выбрать БД"
        /// </summary>
        private void ChooseDB_Button_Click(object sender, EventArgs e)
        {
            var tempDBpath = methods.GetSQLDBpath(this);
            if (tempDBpath == null) return;
            DBpath = tempDBpath;
            DBConnectionString = methods.GetSQLDBconnectionString(DBpath);
            DBpath_Label.Text = DBpath;

            var names = methods.GetTableNamesFromBD(DBConnectionString);
            if (names == null) return;
            tableForInserting_ComboBox.Items.Clear();
            foreach (var name in names)
            {
                tableForInserting_ComboBox.Items.Add(name.ToString());
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Обновить"
        /// </summary>
        private void RefreshTable_Button_Click(object sender, EventArgs e)
        {
            if (isChanged)
            {
                var result = MessageBox.Show(
                    "После выполнения данной операции будет выполнено пересчитывание файла!\nВсе внесенные изменения будут утеряны.\nВы уверены?",
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );
                if (result != DialogResult.Yes) return;
            }

            isChanged = false;

            if (isBD)
            {
                List<string> tempDatas = methods.GetNewDataFromDBTable(DBConnectionString, DBpath, currentBDTable_ComboBox.Text);
                if (tempDatas == null) return;
                else datas = tempDatas;

                int count = methods.GetDBTableColumnCount(DBConnectionString, DBpath, currentBDTable_ComboBox.Text);
                if (count == 0)
                {
                    fixedColumnCount = 1;
                }
                else fixedColumnCount = count;
                columnCount = fixedColumnCount;
                if (isColumnFixed_CheckBox.Checked == false) { tempIsColumnFixed = true; isColumnFixed_CheckBox.Checked = true; tempIsColumnFixed = false; }

            }
            else
            {
                isColumnFixed_CheckBox.Checked = false;
                fixedColumnLabel.Text = "";
                datas = methods.GetDataFromTextFile(curSeparator, filePath, datas);
            }
            methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить"
        /// </summary>
        private void ClearTable_Button_Click(object sender, EventArgs e)
        {
            if (isChanged)
            {
                var result = MessageBox.Show(
                    "После выполнения данной операции будет выполнена очистка таблицы!\nВсе внесенные изменения будут утеряны.\nХотите продолжить?",
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );
                if (result != DialogResult.Yes) return;
            }

            isChanged = false;

            previewTable_DataGridView.Rows.Clear();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Загрузить файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFile_Button_Click(object sender, EventArgs e)
        {
            if (isFileLoaded && isChanged)
            {
                string message = "После выполнения данной операции все внесенные изменения будут утеряны.\nВы уверены?";
                var result = methods.CreateWarningMessage(message);
                if (result != DialogResult.Yes) return;
            }

            // Получение файла
            OpenFileDialog openFileDialog = methods.GetFile(this);
            if (openFileDialog == null) return;

            string tempPath = openFileDialog.FileName;
            isBD = false;
            isFileLoaded = false;

            // Определение расширения
            switch (Path.GetExtension(tempPath).ToLower())
            {
                case ".txt":
                case ".csv":
                    {
                        datas = methods.GetDataFromTextFile(curSeparator, tempPath, datas);
                        isBD = false;
                        isBDWasOpened = false;
                        isFileLoaded = true;
                        currentSelectedTable = null;
                        currentBDTable_ComboBox.Items.Clear();
                        currentBDTable_ComboBox.SelectedIndex = -1;
                        break;
                    }

                case ".mdf":
                    {
                        // Указать в виде текущей БД выбранную
                        DBpath = tempPath;
                        DBConnectionString = methods.GetSQLDBconnectionString(DBpath);
                        DBpath_Label.Text = DBpath;

                        // Проверить подключение к БД
                        var result = methods.DBConnectionCheck(DBConnectionString);
                        if (result == false)
                        {
                            MessageBox.Show("Не удалось выполнить подключение к БД!", "Возникла ошибка!");
                            reset();
                            return;
                        }

                        // Получить список таблиц
                        var names = methods.GetTableNamesFromBD(DBConnectionString);
                        if (names == null)
                        {
                            var answer = MessageBox.Show("В данной БД отсутствуют таблицы, желаете создать?", "Возникла ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (answer != DialogResult.Yes) { reset(); return; }

                            // Создать таблицу (окно в новой форме)

                            names = methods.GetTableNamesFromBD(DBConnectionString);
                        }

                        AddTableNamesToComboBox(names, currentBDTable_ComboBox);

                        isBD = true;
                        isBDWasOpened = true;
                        isFileLoaded = true;

                        break;

                        void reset()
                        {
                            DBpath = null;
                            DBConnectionString = null;
                            isBDWasOpened = false;
                            DBpath_Label.Text = "БД не выбрана";
                        }
                    }
                default:
                    {
                        MessageBox.Show("Необрабатываемое расширение!", "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
            }

            ChangeFormVisibility();
            isChanged = false;
            if (isColumnFixed_CheckBox.Checked != true) { tempIsColumnFixed = true; isColumnFixed_CheckBox.Checked = true; tempIsColumnFixed = false; }
            filePath = tempPath;
            CurrentFile_Label.Text = filePath;
            if (isBD)
            {
                var names = methods.GetTableNamesFromBD(DBConnectionString);
                if (names == null) return;
                tableForInserting_ComboBox.Items.Clear();
                foreach (var name in names)
                {
                    tableForInserting_ComboBox.Items.Add(name.ToString());
                }

                currentSelectedTable = null;
                currentBDTable_ComboBox.SelectedIndex = 1;
                return;
            }
            else // TEXT FILE
            {
                fixedColumnCount = columnCount;
                isColumnFixed_CheckBox.Checked = false;
                methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                return;
            }
        }

        // TEXTBOXEs

        /// <summary>
        /// Обработчик изменения поля "Кол-во столбцов"
        /// </summary>
        private void ColumnCount_TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(columnCount_TextBox.Text, out var result);
                columnCount = result;

                if (columnCount == 0) { throw new Exception("Введите натуральное число!"); }

                if (isColumnFixed_CheckBox.Checked == false)
                {
                    fixedColumnCount = columnCount;
                    methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                }
                else // ЕСЛИ ЗАФИКСИРОВАНО
                {
                    if (fixedColumnCount == 0) { fixedColumnCount = columnCount; fixedColumnLabel.Text = ""; isColumnFixed_CheckBox.Checked = false; return; }

                    if (columnCount >= fixedColumnCount)
                    {//      2               1, 
                        methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                    }
                    else // (columnCount < fixedColumnCount)
                    {//      2               3

                        bool isEmpty = IsColumnEmptyCheck();


                        if (isEmpty) // Если пустой столбец
                        {
                            datas = methods.GetDataFromDataGridView(previewTable_DataGridView, columnCount);
                            //MessageBox.Show($"{datas.Count.ToString()} {columnCount}");
                            fixedColumnCount = columnCount;
                            fixedColumnLabel.Text = fixedColumnCount.ToString();
                            methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, false, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                        }
                        else // Если не пустой столбец
                        {
                            var answer = MessageBox.Show(
                               "При выполнении данного действия может произойти нарушение позиционирования заголовков.\nПродолжить?",
                               "Внимание!",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button2
                            );
                            if (answer != DialogResult.Yes) { columnCount_TextBox.Text = (columnCount + 1).ToString(); return; }

                            isColumnFixed_CheckBox.Checked = false;
                            methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                        }

                        //methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                    }

                    bool IsColumnEmptyCheck()
                    {
                        int rowCount = previewTable_DataGridView.RowCount;

                        bool isColumnEmpty = true;

                        // Проверка на пустоту столбца
                        for (int i = 0; i < rowCount; i++)
                        {
                            var value = previewTable_DataGridView.Rows[i].Cells[fixedColumnCount - 1].Value;
                            if (value != null) if (value.ToString().Trim().Replace(" ", "") != "") { return false; }
                        }

                        return isColumnEmpty;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        /// Обработчик изменения поля "Разделитель"
        /// </summary>
        private void Separator_TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Проверка на пустое поле
                if (separator_TextBox.Text.Length == 0) return;
                // Изменение разделителя
                curSeparator = Convert.ToChar(separator_TextBox.Text);

                // Проверка на динамическое обновление
                if (!isDynamicUpdate_CheckBox.Checked) return;
                datas = methods.GetDataFromTextFile(curSeparator, filePath, datas);
                methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        // COMBOBOXEs

        /// <summary>
        /// Обработчик изменения состояния Текущая выбранная таблица
        /// </summary>
        private void CurrentBDTable_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!handleSelection) { handleSelection = true; return; }

                if (DBConnectionString == null && DBpath == null) return;
                if (currentBDTable_ComboBox.Items.Count == 0) return;

                if (!isBDWasOpened) // Если не открытие БД
                {
                    if (handleSelection == true)
                    {
                        if (currentBDTable_ComboBox.Text.Replace(" ", "") == "" || currentBDTable_ComboBox.Text.Replace(" ", "") == "|ТАБЛИЦЫ|" || currentBDTable_ComboBox.Text.Replace(" ", "") == "|ДЕЙСТВИЯ|")
                        {
                            handleSelection = false;
                            currentBDTable_ComboBox.Text = currentSelectedTable;
                        }
                        else
                        {
                            if (isChanged)
                            {
                                // Подтвердить операцию
                                var answer = MessageBox.Show(
                                    "После выполнения данной операции все внесенные изменения будут утеряны.\nВы уверены?",
                                    "Внимание!",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button2
                                );
                                if (answer != DialogResult.Yes)
                                {
                                    if (currentBDTable_ComboBox.Items.Count <= 5) return;
                                    else { handleSelection = false; currentBDTable_ComboBox.Text = currentSelectedTable; return; }
                                }
                            }

                            if (currentBDTable_ComboBox.SelectedIndex >= currentBDTable_ComboBox.Items.Count - 2)
                            {

                                if (currentBDTable_ComboBox.Text == "Создать")
                                {
                                    CreateNewTable();
                                    return;
                                }

                                if (currentBDTable_ComboBox.Text == "Удалить")
                                {

                                    DeleteCurrentTable();
                                    return;
                                }
                            }
                            isChanged = false;
                        }
                    }
                    handleSelection = true;
                }

                currentSelectedTable = currentBDTable_ComboBox.Text;
                if (isChanged) return;

                int count = methods.GetDBTableColumnCount(DBConnectionString, DBpath, currentSelectedTable);
                if (count == 0) { currentBDTable_ComboBox.Text = currentSelectedTable; return; }
                else fixedColumnCount = count; fixedColumnLabel.Text = count.ToString();

                datas = methods.GetNewDataFromDBTable(DBConnectionString, DBpath, currentBDTable_ComboBox.Text);
                isChanged = false;
                isBDWasOpened = false;

                if (!isColumnFixed_CheckBox.Checked == true) { tempIsColumnFixed = true; isColumnFixed_CheckBox.Checked = true; tempIsColumnFixed = false; } //return;
                fixedColumnLabel.Text = fixedColumnCount.ToString();

                if (columnCount_TextBox.Text == fixedColumnCount.ToString())
                {
                    methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
                }
                else columnCount_TextBox.Text = fixedColumnCount.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        /// Создать новую таблицу
        /// </summary>
        /// <param name="isNeededConfirm">Подтверждение операции</param>
        public void CreateNewTable(bool isNeededConfirm = true)
        {
            try
            {
                //MessageBox.Show("В данный момент доступно ограниченное создание таблицы\n(Имя таблицы, имена столбцов и типы данных изменять нельзя)", "Уведомление");

                if (isNeededConfirm)
                {
                    var answer = MessageBox.Show(
                                            "Вы уверены, что хотите создать новую таблицу?",
                                            "Внимание!",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning,
                                            MessageBoxDefaultButton.Button2
                                        );
                    if (answer != DialogResult.Yes)
                    {
                        if (currentBDTable_ComboBox.Items.Count <= 5) return;
                        else { handleSelection = false; currentBDTable_ComboBox.Text = currentSelectedTable; return; }
                    }
                }

                // ПОЛУЧИТЬ ПАРАМЕТРЫ НОВОЙ ТАБЛИЦЫ
                TableParameters.tableName = "Spravochnik";

                
                List<Methods.Parameters> TableColumnParameters = new List<Methods.Parameters>();
                
                /*
                int countOfColumns = 3;
                
                // Для автосоздания колонок таблицы в отдельной форме
                for (int i = 0; i < countOfColumns; i++)
                {
                    // Заполнить поля parameters
                    Methods.Parameters columnParameters = new Methods.Parameters();
                    columnParameters.columnName = "";
                    columnParameters.columnType = "";
                    columnParameters.maxLength = null;
                    columnParameters.columnAutoIncrement = false;
                    columnParameters.columnNotNull = false;
                    columnParameters.columnPrimary = false;
                    columnParameters.columnValues = null;

                    TableColumnParameters.Add(columnParameters);
                }
                */

                // Ручное создание столбцов таблицы

                Methods.Parameters parameters1 = new Methods.Parameters();
                parameters1.columnName = "Код процесса";
                parameters1.columnType = "NVARCHAR";
                parameters1.maxLength = 100;
                parameters1.columnAutoIncrement = false;
                parameters1.columnNotNull = false;
                parameters1.columnPrimary = false;
                parameters1.columnValues = null;
                TableColumnParameters.Add(parameters1);

                Methods.Parameters parameters2 = new Methods.Parameters();
                parameters2.columnName = "Наименование процесса";
                parameters2.columnType = "NVARCHAR";
                parameters2.maxLength = 300;
                parameters2.columnAutoIncrement = false;
                parameters2.columnNotNull = false;
                parameters2.columnPrimary = false;
                parameters2.columnValues = null;
                TableColumnParameters.Add(parameters2);

                Methods.Parameters parameters3 = new Methods.Parameters();
                parameters3.columnName = "Подразделение процесса";
                parameters3.columnType = "NVARCHAR";
                parameters3.maxLength = 200;
                parameters3.columnAutoIncrement = false;
                parameters3.columnNotNull = false;
                parameters3.columnPrimary = false;
                parameters3.columnValues = null;
                TableColumnParameters.Add(parameters3);

                TableParameters.columnParameters = TableColumnParameters;

                var newTableName = methods.CreateNewTable(DBConnectionString, TableParameters);

                if (newTableName == null)
                {
                    if (currentBDTable_ComboBox.Items.Count <= 5) return;
                    else { handleSelection = false; currentBDTable_ComboBox.Text = currentSelectedTable; return; }
                }

                var names = methods.GetTableNamesFromBD(DBConnectionString);
                if (names == null) return;
                tableForInserting_ComboBox.Items.Clear();
                foreach (var name in names)
                {
                    tableForInserting_ComboBox.Items.Add(name.ToString());
                }

                AddTableNamesToComboBox(names, currentBDTable_ComboBox);

                if (newTableName == currentSelectedTable) { handleSelection = false; currentBDTable_ComboBox.Text = currentSelectedTable; return; }

                currentSelectedTable = newTableName;

                isChanged = false;
                currentBDTable_ComboBox.Text = currentSelectedTable;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        ///  Удалить текущую таблицу
        /// </summary>
        public void DeleteCurrentTable()
        {
            try
            {
                if (currentBDTable_ComboBox.Items.Count <= 5)
                {
                    MessageBox.Show(
                    $"В выбранной БД отсутствуют таблицы!",
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                    );
                    return;
                }

                var deleteCurrentTableAnswer = MessageBox.Show(
                    $"Вы уверены, что хотите удалить таблицу '{currentSelectedTable}'?",
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                    );
                if (deleteCurrentTableAnswer != DialogResult.Yes) { handleSelection = false; currentBDTable_ComboBox.Text = currentSelectedTable; return; }

                methods.DeleteCurrentTable(DBConnectionString, DBpath, currentSelectedTable, curSeparator);

                var names = methods.GetTableNamesFromBD(DBConnectionString);
                AddTableNamesToComboBox(names, currentBDTable_ComboBox);
                if (names == null) return;
                tableForInserting_ComboBox.Items.Clear();
                foreach (var name in names)
                {
                    tableForInserting_ComboBox.Items.Add(name.ToString());
                }
                isChanged = false;

                if (currentBDTable_ComboBox.Items.Count <= 5)
                {
                    var createNewTableAnswer = MessageBox.Show(
                    $"В текущей БД отсутствуют таблицы.\nЖелаете создать новую таблицу?",
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                    );
                    if (createNewTableAnswer != DialogResult.Yes) { previewTable_DataGridView.Rows.Clear(); handleSelection = false; return; }

                    // СОЗДАНИЕ НОВОЙ ТАБЛИЦЫ
                    CreateNewTable(false);
                    return;
                }

                
                currentBDTable_ComboBox.SelectedIndex = 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        /// Добавить имена таблиц в список таблиц
        /// </summary>
        /// <param name="tableNames">Имена таблиц</param>
        /// <param name="comboBox">Ссылка на ComboBox</param>
        private void AddTableNamesToComboBox(List<string> tableNames, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("|        ТАБЛИЦЫ           |");

            foreach (string tableName in tableNames)
            {
                currentBDTable_ComboBox.Items.Add(tableName);
            }

            comboBox.Items.Add("");
            comboBox.Items.Add("|        ДЕЙСТВИЯ         |");
            comboBox.Items.Add("Создать");
            comboBox.Items.Add("Удалить");
        }

        // CHECKBOXEs

        /// <summary>
        /// Обработчик изменения флажка "Зафиксировать"
        /// </summary>
        private void IsColumnFixed_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isColumnFixed_CheckBox.Checked == true)
            {
                if (tempIsColumnFixed) return;

                fixedColumnCount = previewTable_DataGridView.ColumnCount;
                fixedColumnLabel.Text = fixedColumnCount.ToString();
                datas = methods.GetDataFromDataGridView(previewTable_DataGridView, fixedColumnCount);
                methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
            }
            else
            {
                fixedColumnLabel.Text = "";
            }
        }

        /// <summary>
        /// Обработчик изменения флажка "Запись в файл"
        /// </summary>
        private void IsWriteIntoFile_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isBD)
            {
                isCreateNewFile_CheckBox.Visible = false;
            }
            else isCreateNewFile_CheckBox.Visible = isWriteIntoFile_CheckBox.Checked;
        }

        /// <summary>
        /// Обработчик изменения флажка "Запись в БД"
        /// </summary>
        private void IsWriteIntoBD_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            chooseDB_Button.Visible = isWriteIntoBD_CheckBox.Checked;
            DBpath_Label.Visible = isWriteIntoBD_CheckBox.Checked;

            if (isWriteIntoBD_CheckBox.Checked)
            {
                if(DBpath != null || DBpath.Replace(" ","") != "" || DBConnectionString != null || DBConnectionString.Replace(" ", "") != "")
                {
                    var names = methods.GetTableNamesFromBD(DBConnectionString);
                    if (names == null) return;
                    tableForInserting_ComboBox.Items.Clear();
                    foreach (var name in names)
                    {
                        tableForInserting_ComboBox.Items.Add(name.ToString());
                    }
                }
            } else tableForInserting_ComboBox.Items.Clear();

            tableForInserting_ComboBox.Visible = isWriteIntoBD_CheckBox.Checked;
            TableForInserting_Label.Visible = isWriteIntoBD_CheckBox.Checked;
        }

        /// <summary>
        /// Обработчик изменения флажка "Динамическое обновление"
        /// </summary>
        private void IsDynamicUpdate_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Если установили галочку, то происходит обновление
            if (isDynamicUpdate_CheckBox.Checked != true) return;
            datas = methods.GetDataFromTextFile(curSeparator, filePath, datas);
            methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
        }

        /// <summary>
        /// Обработчик изменения флажка "Убирать пустые строки"
        /// </summary>
        private void IsDeleteEmptyRows_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isFileLoaded) return;

            methods.WriteDataToTable(previewTable_DataGridView, datas, columnCount, isColumnFixed_CheckBox.Checked, fixedColumnCount, isDeleteEmptyRows_checkBox.Checked);
        }

        /// <summary>
        /// Обработчик изменения флажка "Растягивать по ширине"
        /// </summary>
        private void autoSizeColumnsMode_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSizeColumnsMode_CheckBox.Checked) previewTable_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            else previewTable_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        /// DATAGRIDVIEWs

        /// <summary>
        /// Обработчик изменения данных в таблице
        /// </summary>
        private void PreviewTable_DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isColumnFixed_CheckBox.Checked) datas = methods.GetDataFromDataGridView(previewTable_DataGridView, fixedColumnCount);
            else datas = methods.GetDataFromDataGridView(previewTable_DataGridView, columnCount);
            isChanged = true;
        }


        /// <summary>
        /// Получить количество значений и строк
        /// </summary>
        /// <returns>int[] {dataCount, rowCount, columnCount}</returns>
        private int[] GetDataGridViewProperties()
        {
            int[] values = new int[3];

            try
            {
                int rowCount,
                thisColumnCount = previewTable_DataGridView.ColumnCount,
                datasCount = datas.Count;
                rowCount = previewTable_DataGridView.Rows.Count;

                int deletedRows = 0;

                if (isDeleteEmptyRows_checkBox.Checked == true)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        string nextRow = "";
                        for (int j = 0; j < thisColumnCount; j++)
                        {
                            var value = previewTable_DataGridView.Rows[i].Cells[j].Value;
                            if (value != null) nextRow += value.ToString();
                        }
                        if (nextRow.Trim().Replace(" ", "") == "") deletedRows++;
                    }
                }
                rowCount -= deletedRows;

                values = new int[3] { rowCount * thisColumnCount, rowCount, thisColumnCount };
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); return null; }

            return values;
        }

        /// <summary>
        /// Вывод диалогового окна с информацией о таблице
        /// </summary>
        private void GetInfoAboutDataGridView_button_Click(object sender, EventArgs e)
        {
            var values = GetDataGridViewProperties();
            if (values == null) return;

            string message = $"Ячеек:  {values[0]}\nСтрок:  {values[1]}\nСтолбцов:  {values[2]}";
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // FORMs

        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если не было изменений - разрешить закрытие
            if (!isChanged) return;

            // Подтверждение выхода
            string message = $"Вы не сохранили изменения!\nВыйти из программы?";
            var result = MessageBox.Show(
                    message,
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );
            if (result == DialogResult.No) e.Cancel = true;
        }

        /// <summary>
        /// Изменение видимости полей
        /// </summary>
        private void ChangeFormVisibility()
        {
            ClearFile_Button.Visible = isFileLoaded;
            FilePreview_GroupBox.Visible = isFileLoaded;
            getInfoAboutDataGridView_button.Visible = isFileLoaded;

            if (isBD)
            {
                isCreateNewFile_CheckBox.Visible = false;

                isDynamicUpdate_CheckBox.Checked = false;
                isDynamicUpdate_CheckBox.Visible = false;

                currentBDTable_ComboBox.Visible = true;
                currentBDTable_Label.Visible = true;

                separator_TextBox.Visible = false;
                separator_Label.Visible = false;
            }
            else
            {
                isDynamicUpdate_CheckBox.Visible = true;

                currentBDTable_ComboBox.Visible = false;
                currentBDTable_Label.Visible = false;

                separator_TextBox.Visible = true;
                separator_Label.Visible = true;

                isCreateNewFile_CheckBox.Visible = true;
            }
        }
    }
}