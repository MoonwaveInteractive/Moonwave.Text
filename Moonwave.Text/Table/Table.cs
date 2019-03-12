// (c) 2019 Moonwave Interactive. All rights reserved under MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Moonwave.Text
{
    /// <summary>
    /// Class that creates table formatted text. This class cannot be inherited.
    /// </summary>
    public sealed partial class Table
    {
        /// <summary>
        /// Gets the number of columns in the table.
        /// </summary>
        public int Columns { get; }

        private int entries;

        /// <summary>
        /// Gets the number of entries in the table.
        /// </summary>
        public int Entries => entries;

        private int columnMargin = 5;

        /// <summary>
        /// Gets or sets the spacing between columns.
        /// </summary>
        public int ColumnMargin
        {
            get
            {
                return columnMargin;
            }

            set
            {
                if (value > 0)
                {
                    columnMargin = value;

                    needsBuilt = true;
                }
                else
                {
                    columnMargin = 0;
                }
            }
        }

        private char groupingCharacter = ' ';

        /// <summary>
        /// Gets or sets the character to be displayed in rows between cells.
        /// Can be used for easier readability.
        /// </summary>
        public char GroupingCharacter
        {
            get
            {
                return groupingCharacter;
            }

            set
            {
                groupingCharacter = value;

                needsBuilt = true;
            }
        }

        /// <summary>
        /// Provides several properties for changing the appearance of the header.
        /// </summary>
        public HeaderOptions Header { get; set; }

        /// <summary>
        /// Provides several properties for changing the appearance of the footer.
        /// </summary>
        public FooterOptions Footer { get; set; }

        private List<List<string>> table = new List<List<string>>();

        private List<string> builtTable = new List<string>();

        private bool needsBuilt = false;

        private Stopwatch stopwatch = new Stopwatch();
        
        List<int> columnLengths = new List<int>();
        
        int builtTableMaxRowSize;

        /// <summary>
        /// Adds a row to the main table.
        /// </summary>
        /// <param name="cells">Cells to be included in the row.</param>
        private void AddRow(params string[] cells)
        {
            for (int i = 0; i < Columns; i++)
            {
                table[i].Add(cells[i]);
            }
        }

        /// <summary>
        /// Initializes a new instance of the Table class, and sets all the properties to their initial values.
        /// </summary>
        /// <param name="columns">Title cells to be added to the header.</param>
        public Table(params string[] columns)
        {
            Header = new HeaderOptions(this);
            Footer = new FooterOptions(this);

            if (columns.Contains(null))
            {
                throw new ArgumentException("Cells cannot be null.");
            }

            Columns = columns.Count();

            for (int i = 0; i < Columns; i++)
            {
                table.Add(new List<string>());
            }

            AddRow(columns);
        }

        /// <summary>
        /// Adds a new row to the table.
        /// </summary>
        /// <param name="cells">Cells to be included in the row.</param>
        public void Add(params string[] cells)
        {
            if (cells.Contains(null))
            {
                throw new ArgumentException("Cells cannot be null.");
            }

            if (!table.Any())
            {
                throw new InvalidOperationException("Row could not be added because no columns exist.");
            }

            if (cells.Count() != Columns)
            {
                throw new InvalidOperationException("The amount of cells in a row must be equal to the number of columns.");
            }

            entries += 1;

            AddRow(cells);

            needsBuilt = true;
        }

        /// <summary>
        /// Builds the final table.
        /// </summary>
        private void BuildTable()
        {
            if (Footer.StatsType == FooterStatsType.Advanced)
            {
                stopwatch.Restart();
            }

            StringBuilder builder = new StringBuilder();

            char groupingChar = ' ';
            
            for (int i = 0; i < table.Count; i++)
            {
                columnLengths.Add(table[i].Aggregate(string.Empty, (max, cur) =>
                max.Length > cur.Length ? max : cur).Length;
            }

            for (int i = 0; i < entries + 1; i++)
            {
                builder.Clear();

                if (i > 0)
                {
                    groupingChar = groupingCharacter;
                }

                for (int x = 0; x < Columns; x++)
                {
                    string cell = table[x][i];
                    string padding = string.Empty;

                    if (x < Columns - 1)
                    {
                        padding = new string(groupingChar, columnLengths[x] - cell.Length + ColumnMargin);
                    }

                    builder.Append(cell + padding);
                }

                builtTable.Add(builder.ToString());
            }

            stopwatch.Stop();
                                  
            builtTableMaxRowSize = builtTable.Aggregate(string.Empty, (max, cur) =>
            max.Length > cur.Length ? max : cur).Length;
        }
                                  
        private void StyleFooter()
        {
            string stats = string.Empty;

            if (Footer.StatsType != FooterStatsType.None)
            {
                if (Footer.StatsType == FooterStatsType.Basic)
                {
                    stats = string.Format("Showing [{0}] entries.", entries);
                }
                else
                {
                    stats = string.Format("Showing [{0}] entries in [{1} ms].", entries, stopwatch.ElapsedMilliseconds);
                }
            }

            if (Footer.BorderStyle != FooterBorderStyle.None)
            {
                if (builtTableMaxRowSize < stats.Length)
                {
                    builtTableMaxRowSize = stats.Length;
                }

                if (Footer.BorderStyle == FooterBorderStyle.Single)
                {
                    builtTable.Add(new string('̄', builtTableMaxRowSize));
                    builtTable.Add(stats);
                    
                }
                else
                {
                    if (Footer.StatsType == FooterStatsType.None)
                    {
                        builtTable.Add(new string('̄', builtTableMaxRowSize));
                    }
                    else
                    {
                        string divider = new string('̄', builtTableMaxRowSize);
                        builtTable.Add(divider);
                        builtTable.Add(stats);
                        builtTable.Add(divider);
                    }
                }
            }
        }
               
        private void StyleHeader()                        
        {
            if (Header.BorderStyle != HeaderBorderStyle.None)
            {
                StringBuilder builder = new StringBuilder();

                switch (Header.BorderStyle)
                {
                    case HeaderBorderStyle.Single:
                        builtTable.Insert(1, new string('̄', builtTableMaxRowSize));
                        break;

                    case HeaderBorderStyle.ColumnTitle:
                        for (int i = 0; i < Columns; i++)
                        {
                            builder.Append(new string('̄', table[i][0].Length));

                            if (i < Columns - 1)
                            {
                                builder.Append(new string(' ', columnLengths[i] -
                                table[i][0].Length + columnMargin));
                            }
                        }

                        builtTable.Insert(1, builder.ToString());
                        break;

                    case HeaderBorderStyle.Column:
                        for (int i = 0; i < Columns; i++)
                        {
                            builder.Append(new string('̄', columnLengths[i]));

                            if (i < Columns - 1)
                            {
                                builder.Append(new string(' ', columnMargin));
                            }
                            else
                            {
                                builder.Append(new string('̄', builtTableMaxRowSize - builder.Length));
                            }
                        }

                        builtTable.Insert(1, builder.ToString());
                        break;

                    case HeaderBorderStyle.Dual:
                        string divider = new string('̄', builtTableMaxRowSize);
                        builtTable.Insert(0, divider);
                        builtTable.Insert(2, divider);
                        break;
                }
            }
        }

        /// <summary>
        /// Styles the final table.
        /// </summary>
        private void StyleTable()
        {
            StyleFooter();
            StyleHeader();
        }

        /// <summary>
        /// Creates then styles the final table.
        /// </summary>
        private void CreateTable()
        {
            if (needsBuilt)
            {
                BuildTable();
                StyleTable();

                needsBuilt = false;
            }
        }

        /// <summary>
        /// Returns a string that reperesents the current table.
        /// </summary>
        /// <returns>A string that reperesents the current table.</returns>
        public override string ToString()
        {
            CreateTable();

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < builtTable.Count; i++)
            {
                builder.Append(builtTable[i] + "\n");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Copies the rows of the table to a new <see cref="System.Collections.Generic.List{T}"/>.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.List{T}"/> that contains rows from the table.</returns>
        public List<string> ToList()
        {
            CreateTable();

            return builtTable;
        }

        /// <summary>
        /// Prints the table to the console.
        /// </summary>
        public void Print()
        {
            CreateTable();

            for (int i = 0; i < builtTable.Count; i++)
            {
                Console.WriteLine(builtTable[i]);
            }
        }
    }
}
