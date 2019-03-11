// (c) 2019 Moonwave Interactive. All rights reserved under MIT License.

namespace Moonwave.Text
{
    sealed partial class Table
    {
        /// <summary>
        /// 
        /// </summary>
        public sealed class HeaderOptions
        {
            private Table table;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="table"></param>
            public HeaderOptions(Table table)
            {
                this.table = table;
            }

            HeaderBorderStyle borderStyle = HeaderBorderStyle.None;

            /// <summary>
            /// Gets or sets the border style of the header.
            /// </summary>
            public HeaderBorderStyle BorderStyle
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

        }
    }
}