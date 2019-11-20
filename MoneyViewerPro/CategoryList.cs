using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    class CategoryList
    {
        public List<Category> categoryList { get; set; }

        public CategoryList(List<Category> category)
        {
            categoryList = category;
        }

        public void addCategory(Category category)
        {
            categoryList.Add(category);
        }

        public Category getCategoryById(int id) {
            foreach (Category category in categoryList) {
                if (category.id == id) {
                    return category;
                }
            }
            return null;
        }
    }
}
