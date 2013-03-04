using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.Controls
{
    public class GuiLib
    {
        /*
        public static IList<CustomListItem<CustomListItemRow>> CreateCustomItemsList(IList<Preset> objects)
        {
            IList<CustomListItem<CustomListItemRow>> items = new List<CustomListItem<CustomListItemRow>>();
            IList<CustomListItemRow> itemRows;

            System.Resources.ResourceManager commonDataResMgr = new System.Resources.ResourceManager("DVDVideoSoft.Resources.CommonData", typeof(DVDVideoSoft.Resources.CommonData).Assembly);
            foreach (Preset p in objects)
            {
                itemRows = new List<CustomListItemRow>();
                itemRows.Add(new CustomListItemRow(p.Name));

                string localizedDescr = commonDataResMgr.GetString(p.Description);
                if (string.IsNullOrEmpty(localizedDescr))
                    localizedDescr = p.Description;

                itemRows.Add(new CustomListItemRow(localizedDescr));

                items.Add(new CustomListItem<CustomListItemRow>(p, itemRows));
            }

            return items;
        }

        public static void FillCustomCombo(ComboBox combo, IList<CustomListItem<CustomListItemRow>> items)
        {
            combo.Items.Clear();
            foreach (CustomListItem<CustomListItemRow> item in items)
            {
                combo.Items.Add(item);
            }
        }
         * */
    }
}
