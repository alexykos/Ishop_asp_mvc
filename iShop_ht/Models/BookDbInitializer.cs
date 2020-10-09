using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iShop_ht.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Classes.Add(new Class { Id = 1, Upcode = 0, Name = "Классика", Isgroup = true });
            db.Classes.Add(new Class { Id = 2, Upcode = 0, Name = "Фантастика", Isgroup = true });
            db.Classes.Add(new Class { Id = 3, Upcode = 1, Name = "Русская", Isgroup = false });
            db.Classes.Add(new Class { Id = 4, Upcode = 1, Name = "Зарубежная", Isgroup = false });
            db.Classes.Add(new Class { Id = 5, Upcode = 2, Name = "Русская", Isgroup = false });
            db.Classes.Add(new Class { Id = 6, Upcode = 2, Name = "Зарубежная", Isgroup = false });

            db.Books.Add(new Book { ClassId = 3, Name = "Война и мир", Author = "Л. Толстой", Price = 220 });
            db.Books.Add(new Book { ClassId = 3, Name = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
            db.Books.Add(new Book { ClassId = 3, Name = "Чайка", Author = "А. Чехов", Price = 150 });

            db.Books.Add(new Book { ClassId = 4, Name = "Полет", Author = "Нортон", Price = 220 });


            db.Books.Add(new Book { ClassId = 5, Name = "Война мага", Author = "Перумов", Price = 220 });
            db.Books.Add(new Book { ClassId = 5, Name = "Клинки", Author = "Садов", Price = 180 });


            db.Books.Add(new Book { ClassId = 6, Name = "Планета", Author = "Гарисон", Price = 220 });
            db.Books.Add(new Book { ClassId = 6, Name = "космолет", Author = "Азимов", Price = 180 });


            base.Seed(db);
        }
    }
}