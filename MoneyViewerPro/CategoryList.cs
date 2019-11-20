using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    static class CategoryList
    {
        public static List<Category> categoryList { get; set; }

        public static void addCategory(Category category)
        {
            categoryList.Add(category);
        }

        public static Category getCategoryById(int id) {
            foreach (Category category in categoryList) {
                if (category.id == id) {
                    return category;
                }
            }
            return null;
        }
    }
}
