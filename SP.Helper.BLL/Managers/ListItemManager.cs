using Microsoft.SharePoint;
using System;

namespace SP.Helper.BLL.Managers
{
    public class ListItemManager
    {
        public int CreateTestData(string spUrl, string listTitle, string fields, int count)
        {
            int addedCount = 0;
            try
            {
                using (SPSite site = new SPSite(spUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList spList = web.Lists[listTitle];
                        var fieldsArray = fields.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        while (addedCount != count)
                        {
                            SPListItem newListItem = spList.Items.Add();

                            foreach (var item in fieldsArray)
                            {
                                newListItem[item] = string.Format("{0} #{1}", item, addedCount);
                            }
                            newListItem.Update();
                            addedCount++;
                        }
                        return addedCount;
                    }
                }
            }
            catch
            {
                return addedCount;
            }
        }
    }
}
