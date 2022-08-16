using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace CSVtoBD
{

    /// <summary>
    /// Класс с методами
    /// </summary>
    public partial class Methods
    {
        // РАБОТА С ТЕКСТОВЫМИ ФАЙЛАМИ (TXT, CSV)

        /// <summary>
        /// Считывание данных из текстового файла
        /// </summary>
        /// <param name="separator">Разделитель</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="currentDatas">Текущий набор данных</param>
        /// <returns>List of string - Считанные данные</returns>
        public List<string> GetDataFromTextFile(char separator, string filePath, List<string> currentDatas = null)
        {
            List<string> datas = new List<string>();

            try
            {
                if (File.Exists(filePath) == false) { MessageBox.Show("Указанный файл более недоступен или не существует.", "Возникла ошибка!"); return currentDatas; }

                using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    string line,
                        savedLine = "";
                    int quotationCount = 0,
                        sepCount = 0;

                    datas.Clear();
                    while ((line = reader.ReadLine()) != null)
                    {
                        foreach (char c in line)
                        {
                            if (c == '"' || c == '\'') quotationCount++;
                            if (c == separator) { sepCount++; }
                        }

                        if (
                            quotationCount % 2 == 0
                            )
                        {
                            string fullLine = savedLine + line;
                            string[] splittedLine = fullLine.Split(separator, '\n');
                            foreach (string s in splittedLine)
                            {
                                string finalLine = "";
                                foreach (char c in s)
                                {
                                    if ((c != '"') && (c != '\'')) finalLine += c;
                                }

                                datas.Add(finalLine);
                            }
                            savedLine = "";
                        }
                        else
                        {
                            savedLine += line;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Возникла ошибка!");
            }

            return datas;
        }

        /// <summary>
        /// Получить путь до нового текстового файла
        /// </summary>
        /// <param name="form">Ссылка на форму</param>
        /// <returns>string - Путь до файла</returns>
        public string GetNewFilePath(Form form)
        {
            string newFilePath = "";

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    Title = "Выберите расположение нового файла"
                };
                var result = saveFileDialog.ShowDialog(form);
                if (result == DialogResult.Cancel) { return newFilePath; }

                newFilePath = saveFileDialog.FileName;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            return newFilePath;
        }

        /// <summary>
        /// Запись в текстовый файл
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="datas">Список данных для записи</param>
        /// <param name="columnCountInt">Количество столбцов</param>
        /// <param name="separator">Разделитель</param>
        public void WriteDataToTextFile(string filePath, List<string> datas, int columnCountInt, char separator)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.Default))
                {

                    string nextLine = "";
                    int dataCount = datas.Count,
                        columnCount = columnCountInt,
                        rowCount,
                        dataLeft = dataCount;

                    if (dataCount % columnCount == 0)
                        rowCount = dataCount / columnCount;
                    else
                        rowCount = (dataCount / columnCount) + 1;

                    if (dataLeft == 0) { writer.WriteLine(""); return; }

                    int dataIndex = 0;
                    for (int j = 0; j < rowCount; j++)
                    {
                        // Формирование строки
                        for (int i = 0; i < columnCount; i++)
                        {
                            // Проверка значения
                            if (datas[dataIndex].Replace(" ", "") != "") nextLine += datas[dataIndex].Trim();
                            else nextLine += "";

                            // Если не последнее значение то добавление разделителя
                            if (i != columnCount - 1) nextLine += separator;
                            dataIndex++;
                        }

                        // Если не пустая строка
                        if ((nextLine.Replace(separator.ToString(), "")).Replace(" ", "") != "")
                        {
                            writer.WriteLine(nextLine);
                        }
                        nextLine = "";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        // РАБОТА С БД

        /// <summary>
        /// Класс для работы с SQL БД
        /// </summary>
        public class SQLStandardMethods
        {
            /// <summary>
            /// Строка подключения к БД
            /// </summary>
            private string DBConnectionString;

            /// <summary>
            /// Подключение к БД
            /// </summary>
            private SqlConnection connection = null;

            /// <summary>
            /// Установить строку подключения и создать подключение к БД
            /// </summary>
            /// <param name="DBConnectionString">Строка подключения к БД</param>
            public void SetDBConnectionString(string DBConnectionString)
            {
                try
                {
                    this.DBConnectionString = DBConnectionString;
                    connection = new SqlConnection(this.DBConnectionString);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            }

            /// <summary>
            /// Получить подключение к БД
            /// </summary>
            /// <returns>SqlConnection - Подключение к БД</returns>
            public SqlConnection GetConnection()
            {
                return connection;
            }

            /// <summary>
            /// Выполнить READ запрос
            /// </summary>
            /// <param name="command">Строка SQL-запроса</param>
            /// <returns>List string[] - Результат выполнения запроса</returns>
            public List<string[]> UseReadCommand(string command)
            {
                List<string[]> list = new List<string[]>();

                try
                {
                    OpenConnection();
                    SqlCommand sqlCommand = new SqlCommand(command, connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] data = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data.SetValue((reader.GetValue(i).ToString()), i);
                        }
                        list.Add(data);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
                finally { CloseConnection(); }

                return list;
            }

            /// <summary>
            /// Выполнить WRITE запрос
            /// </summary>
            /// <param name="commands">Строка SQL-запроса</param>
            public void UseWriteCommand(List<SqlCommand> commands)
            {
                try
                {
                    OpenConnection();
                    foreach (SqlCommand cmd in commands)
                    {
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }                    
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
                finally { CloseConnection(); }
            }

            /// <summary>
            /// Выполнить COUNT запрос
            /// </summary>
            /// <param name="command">Строка запроса</param>
            /// /// <returns>int</returns>
            public int UseCountCommand(string command)
            {
                int result = 0;
                try
                {
                    OpenConnection();
                    SqlCommand sqlCommand = new SqlCommand(command, connection);
                    result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
                finally { CloseConnection(); }

                return result;
            }

            /// <summary>
            /// Создание или удаление таблицы
            /// </summary>
            /// <param name="command">Строка SQL-запроса</param>
            public void MakeGlobalActions(string command)
            {
                try
                {
                    OpenConnection();
                    SqlCommand sqlCommand = new SqlCommand(command, connection);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
                finally { CloseConnection(); }
            }

            /// <summary>
            /// Выполнить подключение к БД
            /// </summary>
            private void OpenConnection()
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            }

            /// <summary>
            /// Закрыть подключение к БД
            /// </summary>
            private void CloseConnection()
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        SqlConnection.ClearPool(connection);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            }
        }

        /// <summary>
        /// Параметры столбцов таблицы
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Имя столбца
            /// </summary>
            public string columnName { get; set; } = null;
            /// <summary>
            /// Тип данных столбца
            /// </summary>
            public string columnType { get; set; } = "NCHAR";   // "NCHAR(n)" or "DATE" or Other
            /// <summary>
            /// Максимальная длина
            /// </summary>
            public int? maxLength { get; set; } = null;
            /// <summary>
            /// Запретить пустые значения
            /// </summary>
            public bool columnNotNull { get; set; } = false;
            /// <summary>
            /// Первичный ключ
            /// </summary>
            public bool columnPrimary { get; set; } = false;
            /// <summary>
            /// Автозаполнение (Только если тип поля INT)
            /// </summary>
            public bool columnAutoIncrement { get; set; } = false;
            /// <summary>
            /// Значения столбца
            /// </summary>
            public List<string> columnValues { get; set; } = null;
        }

        /// <summary>
        /// Свойства таблицы
        /// </summary>
        public struct TableParameters
        {
            /// <summary>
            /// Имя таблицы
            /// </summary>
            public string tableName;
            /// <summary>
            /// Параметры таблицы
            /// </summary>
            public List<Parameters> columnParameters;
        }

        /// <summary>
        /// Создать новую таблицу
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="newTableParameters">Параметры таблицы в виде структуры NewTableParameters</param>
        /// <returns>Имя текущей выбранной таблицы или новой</returns>
        public string CreateNewTable(string DBConnectionString, TableParameters newTableParameters)
        {
            try
            {
                // Проверка на совпадение имени
                var currentTableNames = GetTableNamesFromBD(DBConnectionString);
                if (currentTableNames != null)
                {
                    foreach (string name in currentTableNames)
                    {
                        if (name == newTableParameters.tableName)
                        {
                            MessageBox.Show("Таблица с таким именем уже существует!", "Возникла ошибка!");
                            return name;
                        }
                    }
                }

                // Создание таблицы

                if (newTableParameters.columnParameters.Count == 0)
                {
                    MessageBox.Show("Не указаны параметры таблицы!", "Возникла ошибка!");
                    return null;
                }

                string columnParameters = "";
                int index = 1,
                    columnCount = newTableParameters.columnParameters.Count;
                foreach (var parameters in newTableParameters.columnParameters)
                {
                    if (parameters.columnName == null || parameters.columnName.Replace(" ", "") == "")
                    {
                        MessageBox.Show("Не указано имя столбца!", "Возникла ошибка!");
                        return null;
                    }
                    if (parameters.columnType == null || parameters.columnType.Replace(" ", "").Replace("[", "").Replace("]", "") == "")
                    {
                        MessageBox.Show($"Не указан тип данных столбца {parameters.columnName}!", "Возникла ошибка!");
                        return null;
                    }

                    // ИМЯ СТОЛБЦА
                    columnParameters += $"[{parameters.columnName.Trim()}] ";
                    // ТИП ДАННЫХ СТОЛБЦА
                    columnParameters += $"[{parameters.columnType.Trim()}]";
                    // Максимальная длина
                    if (parameters.maxLength != 0 && parameters.maxLength != null)
                        columnParameters += $"({parameters.maxLength}) ";
                    else columnParameters += " ";
                    // AUTO INCREMENT
                    if (parameters.columnAutoIncrement && parameters.columnType.ToUpper().Trim().Replace("[", "").Replace("]", "") == "INT") columnParameters += $"IDENTITY(1,1) ";
                    // РАЗРЕШЕНИЕ ПУСТЫХ ЗНАЧЕНИЙ
                    if (parameters.columnNotNull || parameters.columnAutoIncrement || parameters.columnPrimary) columnParameters += $"NOT NULL ";
                    else columnParameters += $"NULL ";
                    // ПЕРВИЧНЫЙ КЛЮЧ
                    if (parameters.columnPrimary)
                        columnParameters += $"PRIMARY KEY";

                    if (index < columnCount) columnParameters += ",";
                    index++;
                }

                string command =
                    $"CREATE TABLE [{newTableParameters.tableName}] " +
                    $"({columnParameters})";

                CreateTable(DBConnectionString, command);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return newTableParameters.tableName;
        }

        /// <summary>
        /// Удалить таблицу
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="DBpath">Путь до файла БД</param>
        /// <param name="tableName">Имя удаляемой таблицы</param>
        /// <param name="separator">Разделитель</param>
        public void DeleteCurrentTable(string DBConnectionString, string DBpath, string tableName, char separator)
        {
            try
            {
                if (tableName == null || tableName.Trim().Replace(" ", "") == "")
                {
                    MessageBox.Show("Имя таблицы не указано!", "Возникла ошибка!");
                }
                //DROP TABLE IF EXISTS T1;  
                string command = $"DROP TABLE IF EXISTS [{tableName}]";

                string backupFolderPath = $"{Environment.CurrentDirectory}" + @"\Backups\".Replace(@"\", "/");

                // БЭКАП УДАЛЯЕМОЙ ТАБЛИЦЫ


                int columnCount = GetDBTableColumnCount(DBConnectionString, DBpath, tableName);

                List<string> datas = GetNewDataFromDBTable(DBConnectionString, DBpath, tableName);


                string backupFilePath = (backupFolderPath + $"{tableName} Backup-{DateTime.Now.ToString("dd-MM-yyyy hh-mm")}.CSV").Replace(@"\", "/");

                if (!Directory.Exists(backupFolderPath)) Directory.CreateDirectory(backupFolderPath);
                WriteDataToTextFile(backupFilePath, datas, columnCount, separator);
                MessageBox.Show($"Путь до резервной копии:\n{backupFilePath}", "Создана резервная копия");

                DeleteTable(DBConnectionString, command);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        /// Получить полную информацию о таблице
        /// </summary>
        public TableParameters GetFullTableData(string DBConnectionString, string tableName)
        {
            TableParameters table = new TableParameters();
            try
            {
                // work
                // Получение информации о столбцах таблицы
                string getColumnsPropertiesCommand = $"select COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{tableName}'";

                // work
                // Определение столбцов с автозаполнением
                string getColumnsWithIdentityCommand =
                    $"SELECT COLUMN_NAME " +
                    $"FROM INFORMATION_SCHEMA.COLUMNS " +
                    $"WHERE COLUMNPROPERTY(object_id(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 " +
                    $"AND " +
                    $"TABLE_NAME = '{tableName}'";

                // Определение primary столбцов
                string getPrimaryColumns =
                    @"SELECT COLUMN_NAME
                    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                    WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1" +
                    $"AND TABLE_NAME = '{tableName}'";

                string getData = $"SELECT * FROM {tableName}";

                // Получение данных из столбцов таблицы
                List<string[]> columnsInfo = GetDataFromBD(DBConnectionString, getColumnsPropertiesCommand);
                List<string[]> columnsWithIdentity = GetDataFromBD(DBConnectionString, getColumnsWithIdentityCommand);
                List<string[]> columnsWithPrimary = GetDataFromBD(DBConnectionString, getPrimaryColumns);
                List<string[]> tableColumnData = GetDataFromBD(DBConnectionString, getData);

                table.tableName = tableName;
                table.columnParameters = new List<Parameters>();

                Parameters parameters;
                for (int i = 0; i < columnsInfo.Count; i++)
                {
                    string[] curColInfo = columnsInfo[i];
                    // 0 col name
                    // 1 col type
                    // 2 max len
                    // 3 is nullable

                    parameters = new Parameters();
                    parameters.columnName = curColInfo[0];
                    parameters.columnType = curColInfo[1];

                    if (int.TryParse(curColInfo[2], out int count))
                    {
                        parameters.maxLength = count;
                    }

                    parameters.columnNotNull = curColInfo[3].ToUpper() == "YES";

                    parameters.columnPrimary = false;
                    if (columnsWithPrimary != null)
                    {
                        foreach (string name in columnsWithPrimary[0])
                        {
                            if (name == parameters.columnName) { parameters.columnPrimary = true; break; }
                        }
                    }
                    parameters.columnAutoIncrement = false;
                    if (columnsWithIdentity != null)
                    {
                        foreach (string name in columnsWithIdentity[0])
                        {
                            if (name == parameters.columnName) { parameters.columnAutoIncrement = true; break; }
                        }
                    }

                    if (tableColumnData != null)
                    {
                        List<string> columnData = new List<string>();
                        foreach (string data in tableColumnData[i])
                        {
                            columnData.Add(data);
                        }
                        parameters.columnValues = columnData;
                    }
                    else { parameters.columnValues = null; }
                    table.columnParameters.Add(parameters);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            return table;
        }

        /// <summary>
        /// Получить данные из таблицы вместе с заголовками
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="DBpath">Путь до БД</param>
        /// <returns>List string</returns>
        public List<string> GetNewDataFromDBTable(string DBConnectionString, string DBpath, string tableName)
        {
            List<string> newData = new List<string>();
            try
            {
                if (File.Exists(DBpath) == false) { MessageBox.Show("Файл БД более недоступен или несуществует.", "Возникла ошибка!"); return null; }

                var columnNames = GetDBTableColumnNames(DBConnectionString, DBpath, tableName);
                if (columnNames != null)
                {
                    foreach (string columnName in columnNames)
                    {
                        newData.Add(columnName);
                    }
                }
                else
                {
                    return null;
                }

                var columnData = GetDBTableColumnData(DBConnectionString, DBpath, tableName);
                if (columnData != null)
                {
                    foreach (string cell in columnData)
                    {
                        newData.Add(cell);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            return newData;
        }

        /// <summary>
        /// Получить число столбцов таблицы
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="DBpath">Путь до БД</param>
        /// <returns>int</returns>
        public int GetDBTableColumnCount(string DBConnectionString, string DBpath, string tableName)
        {
            int tableColumnCount = 0;
            try
            {
                if (File.Exists(DBpath) == false) { MessageBox.Show("Файл БД более недоступен или несуществует.", "Возникла ошибка!"); return tableColumnCount; }

                string command;

                command = $"SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME='{tableName}'";
                tableColumnCount = GetCountFromBD(DBConnectionString, command);
                if (tableColumnCount == 0)
                {
                    MessageBox.Show(
                        "В выбранной таблице отсутстсвуют столбцы!",
                        "Внимание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return tableColumnCount;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            return tableColumnCount;
        }

        /// <summary>
        /// Получить заголовки таблицы
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="DBpath">Путь до БД</param>
        /// <returns>List string</returns>
        public List<string> GetDBTableColumnNames(string DBConnectionString, string DBpath, string tableName)
        {
            List<string> list = new List<string>();

            try
            {
                if (File.Exists(DBpath) == false) { MessageBox.Show("Файл БД более недоступен или несуществует.", "Возникла ошибка!"); return null; }

                // Получение имен столбцов таблицы
                string command = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name='{tableName}'";
                var templist = GetDataFromBD(DBConnectionString, command);
                if (templist == null) return null;
                else
                {
                    foreach (var row in templist)
                    {
                        foreach (var cell in row)
                        {
                            if (cell == null) list.Add("");
                            else list.Add(cell.ToString());
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            return list;
        }

        /// <summary>
        /// Получить значения столбцов таблицы
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="DBpath">Путь до БД</param>
        /// <returns>List string</returns>
        public List<string> GetDBTableColumnData(string DBConnectionString, string DBpath, string tableName)
        {
            List<string> list = new List<string>();
            try
            {
                if (File.Exists(DBpath) == false) { MessageBox.Show("Файл БД более недоступен или несуществует.", "Возникла ошибка!"); return null; }

                string command = $"SELECT * FROM [{tableName}]";
                var templist = GetDataFromBD(DBConnectionString, command);
                if (templist == null) return list;

                foreach (var row in templist)
                {
                    foreach (var cell in row)
                    {
                        if (cell == null) list.Add("");
                        else list.Add(cell.ToString());
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
            return list;
        }

        /// <summary>
        /// Получить имена таблиц из БД
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения</param>
        /// <returns>List string / null</returns>
        public List<string> GetTableNamesFromBD(string DBConnectionString)
        {
            List<string> tableNames = new List<string>();

            try
            {
                string command = "SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_TYPE LIKE '%TABLE%'";

                SQLStandardMethods sqlBD = new SQLStandardMethods();

                sqlBD.SetDBConnectionString(DBConnectionString);

                var result = sqlBD.UseReadCommand(command);

                if (result.Count == 0) { return null; }

                foreach (string[] strings in result)
                {
                    foreach (string str in strings)
                    {
                        tableNames.Add(str);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return tableNames;
        }

        /// <summary>
        /// Получить путь до БД
        /// </summary>
        /// <param name="form">Ссылка на форму</param>
        /// <returns>Путь до файла или null</returns>
        public string GetSQLDBpath(Form form)
        {
            string DBpath = null;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Выберите файл БД",
                    Filter = "MDF files (*.mdf)|*.mdf|All files (*.*)|*.*"
                };
                var result = openFileDialog.ShowDialog(form);
                if (result == DialogResult.Cancel) { return null; }

                DBpath = openFileDialog.FileName;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return DBpath;
        }

        /// <summary>
        /// Получить подключение к БД
        /// </summary>
        /// <param name="DBpath"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetSQLDBconnectionString(string DBpath, string login = "", string password = "")
        {
            string DBConnectionString = "";

            try
            {
                string start = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + "\"",
                       middle = login + password, //для логина и пароля
                                                  //DBpath
                       end = "\";Integrated Security=True;Connect Timeout=30";
                DBConnectionString = start + middle + DBpath + end;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return DBConnectionString;
        }

        /// <summary>
        /// Записать данные в БД
        /// </summary>
        /// <param name="DBpath">Путь до БД</param>
        /// <param name="DBConnectionString">Строка подключения</param>
        /// <param name="datas">Данные</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="grid">Ссылка на DataGridView</param>
        public void WriteDataToDB(string DBConnectionString, DataGridView grid, List<string> datas, string DBpath, string tableName)
        {
            try
            {
                SQLStandardMethods sqlBD = new SQLStandardMethods();
                sqlBD.SetDBConnectionString(DBConnectionString);

                // Данные таблицы БД
                TableParameters tableParameters = GetFullTableData(DBConnectionString, tableName);
                List<Parameters> columnParameters = tableParameters.columnParameters;

                if (columnParameters == null)
                {
                    MessageBox.Show("Не удалось получить данные из таблицы БД.", "Возникла ошибка!");
                    return;
                }
                int columnCount = grid.ColumnCount;
                // Проверка совпадения числа столбцов
                if (columnCount != columnParameters.Count)
                {
                    MessageBox.Show("Число столбцов таблицы не совпадает с числом столбцов в выбранной таблице БД", "Возникла ошибка!");
                    return;
                }

                // Проверка наличия строк
                if (grid.Rows.Count == 0)
                {
                    MessageBox.Show("В таблице отсутствуют строки!", "Возникла ошибка!");
                    return;
                }

                List<string> withoutIncrement = new List<string>();
                
                // Проверка совпадения имен столбцов
                for (int i = 0; i < columnCount; i++)
                {
                    Parameters columnParameter = columnParameters[i];
                    
                    var value = grid.Rows[0].Cells[i].Value;
                    if (value == null || value.ToString().Trim() != columnParameters[i].columnName.ToString().Trim())
                    {
                        MessageBox.Show("Имена столбцов таблиц не совпадают!", "Возникла ошибка!");
                        return;
                    }

                    if (columnParameter.columnAutoIncrement == false) 
                    { 
                        withoutIncrement.Add(columnParameter.columnName.ToString().Trim());
                    }
                }

                // Проверка на наличие данных
                if(datas.Count == 0)
                {
                    MessageBox.Show("Отсутствуют данные для записи!", "Возникла ошибка!");
                    return;
                }

                // Формирование команд
                SqlCommand command = new SqlCommand();
                command.CommandText += $"INSERT INTO [{tableName}](";

                int index = 0;
                foreach(string name in withoutIncrement)
                {
                    command.CommandText += $"[{name}]";
                    if (index < withoutIncrement.Count-1) command.CommandText += ",";
                    index++;
                }
                command.CommandText += ") VALUES (";

                index = 0;
                List<int> autoIncrement = new List<int>();
                List<int> notNull = new List<int>();
                foreach (Parameters parameter in columnParameters)
                {
                    if (parameter.columnAutoIncrement == false) { autoIncrement.Add(index); }
                    if (parameter.columnNotNull == true) { notNull.Add(index); }
                }

                // Формирование списка команд
                index = 0;
                List<SqlCommand> listOfCommands = new List<SqlCommand>();
                SqlCommand deleteAll = new SqlCommand();
                deleteAll.CommandText = $"DELETE FROM [{tableName}]";
                listOfCommands.Add(deleteAll);
                for (int i = 0; i < datas.Count; i+=columnCount)
                {
                    string newCommandPart = "";
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (notNull.Contains(j + 1))
                        {
                            if (datas[index].Replace(" ", "") == "")
                            {
                                MessageBox.Show($"В столбце {j + 1} не дожно быть пустых значений!", "Возникла ошибка!");
                                return;
                            }
                        }

                        if (!autoIncrement.Contains(j + 1)) 
                        { 
                            newCommandPart += $"N'{datas[index]}'";
                            if (j < columnCount - 1) newCommandPart += ",";
                        }
                        index++;
                    }
                    newCommandPart += ")";
                    SqlCommand newCommand = new SqlCommand();
                    newCommand.CommandText += command.CommandText + newCommandPart;
                    listOfCommands.Add(newCommand);
                }

                sqlBD.UseWriteCommand(listOfCommands);

                MessageBox.Show($"Значения успешно добавлены в таблицу {tableName}","Уведомление");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        /// Проверить строку подключения к БД SQL
        /// </summary>
        /// <param name="connectionStringForTest">Строка подключения</param>
        /// <returns>true/false</returns>
        public bool DBConnectionCheck(string connectionStringForTest)
        {
            // Проверка строки подключения
            if (connectionStringForTest == "") return false;
            else return true;
        }

        /// <summary>
        /// Получить скалярное значение из БД
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="command">Команда</param>
        /// <returns>int</returns>
        private int GetCountFromBD(string DBConnectionString, string command)
        {
            int count = 0;

            try
            {
                SQLStandardMethods sqlBD = new SQLStandardMethods();
                sqlBD.SetDBConnectionString(DBConnectionString);
                count = sqlBD.UseCountCommand(command);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return count;
        }

        /// <summary>
        /// Получить данные из БД
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="command">Команда</param>
        /// <returns>List string[] / null</returns>
        private List<string[]> GetDataFromBD(string DBConnectionString, string command)
        {
            List<string[]> commandResult = new List<string[]>();

            try
            {
                SQLStandardMethods sqlBD = new SQLStandardMethods();
                sqlBD.SetDBConnectionString(DBConnectionString);
                var result = sqlBD.UseReadCommand(command);

                if (result.Count == 0) { return null; }
                commandResult = result;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return commandResult;
        }

        /// <summary>
        /// Создать таблицу
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="command">Строка SQL-запроса с созданием таблицы</param>
        private void CreateTable(string DBConnectionString, string command)
        {
            try
            {
                SQLStandardMethods sqlBD = new SQLStandardMethods();
                sqlBD.SetDBConnectionString(DBConnectionString);
                sqlBD.MakeGlobalActions(command);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        /// <summary>
        /// Удалить таблицу
        /// </summary>
        /// <param name="DBConnectionString">Строка подключения к БД</param>
        /// <param name="command">Команда SQL-запроса с удалением таблицы</param>
        private void DeleteTable(string DBConnectionString, string command)
        {
            try
            {
                SQLStandardMethods sqlBD = new SQLStandardMethods();
                sqlBD.SetDBConnectionString(DBConnectionString);
                sqlBD.MakeGlobalActions(command);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }
        }

        // РАБОТА С ТАБЛИЦЕЙ

        /// <summary>
        /// Считывание DataGridView
        /// </summary>
        /// <param name="grid">DataGridView</param>
        /// <param name="columnCountInt">Количество столбцов</param>
        /// <returns>Считанные данные</returns>
        public List<string> GetDataFromDataGridView(DataGridView grid, int columnCountInt)
        {
            List<string> fileData = new List<string>();

            try
            {
                if (columnCountInt <= 0)
                {
                    throw new Exception("Введите натуральное число!");
                }

                int rowCount = grid.RowCount - 1,
                    columnCount = columnCountInt;

                // Перебор строк
                for (int j = 0; j < rowCount; j++)
                {
                    // Перебор столбцов
                    for (int i = 0; i < columnCount; i++)
                    {
                        var value = grid.Rows[j].Cells[i].Value;

                        if (value != null) fileData.Add(value.ToString());
                        else fileData.Add("");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return fileData;
        }

        /// <summary>
        /// Заполнение таблицы
        /// </summary>
        /// <param name="grid">Таблица</param>
        /// <param name="datas">Данные</param>
        /// <param name="isFixed">Проверка фиксации</param>
        /// <param name="columnCountInt">Количество столбцов</param>
        /// <param name="fixedColumnCount">Количество зафиксированных столбцов</param>
        /// <param name="isDeleteEmptyRows">Убирать пустые строки</param>
        /// <returns>int[] {dataCount, rowCount}</returns>
        public void WriteDataToTable(DataGridView grid, List<string> datas, int columnCountInt, bool isFixed, int fixedColumnCount, bool isDeleteEmptyRows)
        {
            int dataCount,
                rowCount,
                deletedRowCount = 0,
                columnCount;

            try
            {
                if (columnCountInt <= 0)
                {
                    MessageBox.Show(
                    "Введите натуральное число!",
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                    return;
                }

                dataCount = datas.Count;
                int dataIndex, dataLeft, rowIndex;

                grid.Rows.Clear();
                grid.ColumnCount = columnCountInt;

                // Если данных нет
                dataLeft = dataCount;
                if (dataLeft == 0) { grid.Rows.Add(); return; }

                columnCount = isFixed ? fixedColumnCount : columnCountInt;

                if (dataCount % columnCount == 0)
                    rowCount = dataCount / columnCount;
                else
                    rowCount = (dataCount / columnCount) + 1;

                dataIndex = 0;
                rowIndex = 0;

                for (int i = 0; i < rowCount; i++)
                {
                    string nextRow = "";
                    int maxValue,
                        tempIndex = dataIndex;

                    if (dataLeft > columnCount) { dataLeft -= columnCount; maxValue = columnCount; }
                    else { maxValue = dataLeft; }

                    // Проверка строки на пустоту
                    if (isDeleteEmptyRows)
                    {
                        for (int j = 0; j < maxValue; j++)
                        {
                            nextRow += datas[tempIndex];
                            tempIndex++;
                        }
                        var value = nextRow.Trim().Replace(" ", "");

                        // Запись в таблицу

                        if (value != "")
                        {
                            rowAdd();
                        }
                        else { dataIndex = tempIndex; deletedRowCount++; }
                    }
                    else
                    {
                        rowAdd();
                    }

                    void rowAdd()
                    {
                        grid.Rows.Add();

                        for (int j = 0; j < maxValue; j++)
                        {
                            grid.Rows[rowIndex].Cells[j].Value = datas[dataIndex];
                            dataIndex++;
                        }
                        rowIndex++;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Возникла ошибка!"); }

            return;
        }

        // ИНСТРУМЕНТАЛЬНОЕ

        /// <summary>
        /// Создание окна подтверждения
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        /// <returns>Ответ пользователя</returns>
        public DialogResult CreateWarningMessage(string message)
        {
            var result = MessageBox.Show(
                    message,
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );
            return result;
        }

        /// <summary>
        /// Получить путь до файла
        /// </summary>
        /// <param name="form">Ссылка на форму</param>
        /// <returns>openFileDialog / null</returns>
        public OpenFileDialog GetFile(Form form)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите файл для считывания",
                Filter = "CSV files (*.csv)|*.csv|MDF files (*.mdf)|*.mdf|All files (*.*)|*.*"
            };
            var result = openFileDialog.ShowDialog(form);
            if (result == DialogResult.Cancel) { return null; }

            return openFileDialog;
        }
    }
}