// (c) 2019 Moonwave Interactive. All rights reserved under MIT License.

namespace Moonwave.Text
{
    sealed partial class Table
    {
        /// <summary>
        /// 
        /// </summary>
        public sealed class FooterOptions
        {
            private Table table;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="table"></param>
            public FooterOptions(Table table)
            {
                this.table = table;
            }

            FooterBorderStyle borderStyle = FooterBorderStyle.None;

            /// <summary>
            /// Gets or sets the border style of the footer.
            /// </summary>
            public FooterBorderStyle BorderStyle
            {
                get
                {
                    return borderStyle;
                }

                set
                {
                    borderStyle = value;

                    table.needsBuilt = true;
                }
            }

            FooterStatsType statsType = FooterStatsType.None;

            /// <summary>
            /// Gets or sets the type of statistics to be displayed in the footer.
            /// </summary>
            public FooterStatsType StatsType
            {
                get
                {
                    return statsType;
                }

                set
                {
                    statsType = value;

                    table.needsBuilt = true;
                }
            }

        }
    }
}