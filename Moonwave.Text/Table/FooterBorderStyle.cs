// (c) 2019 Moonwave Interactive. All rights reserved under MIT License.

namespace Moonwave.Text
{
    /// <summary>
    /// Specifies constants that define the border style of the footer.
    /// </summary>
    public enum FooterBorderStyle
    {
        /// <summary>
        /// Disables viewing of the footers border.
        /// </summary>
        None = 0,

        /// <summary>
        /// Displays a single line divider under the footer.
        /// </summary>
        Single = 1,

        /// <summary>
        /// Displays a single line divider on the top and bottom of the footer, only if
        /// the footer contains statistics, otherwise a single line divider under the footer.
        /// </summary>
        Dual = 2
    }
}