using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace quanlysanxuat.Model
{
    class TablePermission
    {
        private static TablePermission _defaultInstance;
        public static TablePermission Instance
        {
            get
            {
                if (_defaultInstance == null)
                {
                    _defaultInstance = new TablePermission();
                }
                return _defaultInstance;
            }
            set => _defaultInstance = value;
        }

        public TablePermission()
        {

        }
        public DataTable CreatedTablePermission()
        {
            DataTable table_ribbon = new DataTable();
            table_ribbon.Columns.Add("id", typeof(string));
            table_ribbon.Columns.Add("parentid", typeof(string));
            table_ribbon.Columns.Add("caption", typeof(string));
            table_ribbon.Columns.Add("ispage", typeof(int));
            table_ribbon.Columns.Add("isgrouppage", typeof(int));
            table_ribbon.Columns.Add("image", typeof(byte[]));
            table_ribbon.Columns.Add("image_index", typeof(int));
            table_ribbon.Columns.Add("view", typeof(bool));
            table_ribbon.Columns.Add("add", typeof(bool));
            table_ribbon.Columns.Add("edit", typeof(bool));
            table_ribbon.Columns.Add("delete", typeof(bool));
            table_ribbon.Columns.Add("print", typeof(bool));
            table_ribbon.Columns.Add("extra", typeof(bool));
            table_ribbon.Columns.Add("username_edit", typeof(string));
            table_ribbon.Columns.Add("created_date", typeof(DateTime));
            table_ribbon.Columns["view"].DefaultValue = false;
            table_ribbon.Columns["add"].DefaultValue = false;
            table_ribbon.Columns["edit"].DefaultValue = false;
            table_ribbon.Columns["delete"].DefaultValue = false;
            table_ribbon.Columns["print"].DefaultValue = false;
            table_ribbon.Columns["extra"].DefaultValue = false;
            table_ribbon.Columns["image_index"].DefaultValue = -1;
            table_ribbon.Columns["username_edit"].DefaultValue = "";
            table_ribbon.Columns["created_date"].DefaultValue = DateTime.Now;
            return table_ribbon;
        }

        public Tuple<DataTable, ImageCollection> GetAllTableMenu(RibbonControl ribbonControl)
        {

            var imageCollection = new ImageCollection();
            DataTable table_ribbon = CreatedTablePermission();
            ArrayList visiblePages = ribbonControl.TotalPageCategory.GetVisiblePages();
            foreach (RibbonPage page in visiblePages)
            {
                var page_name = page.Name;
                var page_caption = page.Text;
                imageCollection.AddImage(Properties.Resources.edittask_16x16, page_name);
                table_ribbon.Rows.Add(page_name, "0", page_caption, 1, 0, null, imageCollection.Images.Count - 1);

                foreach (RibbonPageGroup group in page.Groups)
                {
                    var page_group_name = group.Name;
                    var page_group_caption = group.Text;
                    if (page_group_name.Equals("ribbonPageGroup_Giaodien"))
                    {
                        break;
                    }
                    imageCollection.AddImage(Properties.Resources.newtask_16x16, page_name);
                    table_ribbon.Rows.Add(page_group_name, page_name, page_group_caption, 0, 1, null, imageCollection.Images.Count - 1);

                    foreach (BarItemLink item in group.ItemLinks)
                    {
                        var item_caption = item.Caption;
                        var item_name = item.Item.Name;

                        if (item.ImageOptions.HasSvgImage)
                        {
                            var item_image = item.ImageOptions.SvgImage;
                            SvgBitmap source = new SvgBitmap(item_image);
                            var imgFromSGV = source.Render(null, 0.5);
                            //var size = new Size(16, 16);
                            //Bitmap target = new Bitmap(size.Width, size.Height);
                            //using (Graphics g = Graphics.FromImage(target))
                            //{
                            //    source.RenderToGraphics(g,
                            //        SvgPaletteHelper.GetSvgPalette(LookAndFeel, ObjectState.Normal));
                            //}
                            imageCollection.AddImage(imgFromSGV, item_caption);
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                item_image.Save(mStream);
                                table_ribbon.Rows.Add(item_name, page_group_name, item_caption, 0, 0, mStream.ToArray(), imageCollection.Images.Count - 1);
                            }
                        }
                        else if (item.ImageOptions.HasImage)
                        {
                            var item_image = item.ImageOptions.Image;
                            imageCollection.AddImage(item_image, item_caption);
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                item_image.Save(mStream, item_image.RawFormat);
                                table_ribbon.Rows.Add(item_name, page_group_name, item_caption, 0, 0, mStream.ToArray(), imageCollection.Images.Count - 1);
                            }
                        }
                        else
                        {
                            table_ribbon.Rows.Add(item_name, page_group_name, item_caption, 0, 0, null);
                        }


                    }

                }
            }

            return Tuple.Create(table_ribbon, imageCollection);
        }
    }
}
