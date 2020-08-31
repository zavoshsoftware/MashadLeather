using System;
using System.Linq;

namespace Models
{
    internal static class DatabaseContextInitializer
    {
        static DatabaseContextInitializer()
        {

        }

        internal static void Seed(DatabaseContext databaseContext)
        {
            ProvinceAndCitySeedData(databaseContext);
            RoleSeed(databaseContext);
            BaseUserSeed(databaseContext);
            OrderStatusSeed(databaseContext);
            ColorSeed(databaseContext);
            SizeSeed(databaseContext);
            databaseContext.SaveChanges();
        }

        public static void ProvinceAndCitySeedData(DatabaseContext databaseContext)
        {
            Guid provinceId = Guid.NewGuid();

            InsertInfoProvinces(provinceId, "آذربایجان شرقی", databaseContext);
            InsertIntoCities(provinceId, "آبش‌احمد", false, databaseContext);
            InsertIntoCities(provinceId, "آذرشهر", false, databaseContext);
            InsertIntoCities(provinceId, "آق‌کند", false, databaseContext);
            InsertIntoCities(provinceId, "اسکو", false, databaseContext);
            InsertIntoCities(provinceId, "اهر", false, databaseContext);
            InsertIntoCities(provinceId, "ایلخچی", false, databaseContext);
            InsertIntoCities(provinceId, "باسمنج", false, databaseContext);
            InsertIntoCities(provinceId, "بخشایش", false, databaseContext);
            InsertIntoCities(provinceId, "بستان‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "بناب", false, databaseContext);
            InsertIntoCities(provinceId, "بناب جدید", false, databaseContext);
            InsertIntoCities(provinceId, "تبریز", true, databaseContext);
            InsertIntoCities(provinceId, "ترک", false, databaseContext);
            InsertIntoCities(provinceId, "ترکمان‌چای", false, databaseContext);
            InsertIntoCities(provinceId, "تسوج", false, databaseContext);
            InsertIntoCities(provinceId, "تیکمه‌داش", false, databaseContext);
            InsertIntoCities(provinceId, "جلفا", false, databaseContext);
            InsertIntoCities(provinceId, "خاروانا", false, databaseContext);
            InsertIntoCities(provinceId, "خامنه", false, databaseContext);
            InsertIntoCities(provinceId, "خراجو", false, databaseContext);
            InsertIntoCities(provinceId, "خسروشهر", false, databaseContext);
            InsertIntoCities(provinceId, "خضرلو", false, databaseContext);
            InsertIntoCities(provinceId, "خمارلو", false, databaseContext);
            InsertIntoCities(provinceId, "خواجه", false, databaseContext);
            InsertIntoCities(provinceId, "دوزدوزان", false, databaseContext);
            InsertIntoCities(provinceId, "زرنق", false, databaseContext);
            InsertIntoCities(provinceId, "زنوز", false, databaseContext);
            InsertIntoCities(provinceId, "سراب", false, databaseContext);
            InsertIntoCities(provinceId, "سردرود", false, databaseContext);
            InsertIntoCities(provinceId, "سهند", false, databaseContext);
            InsertIntoCities(provinceId, "سیس", false, databaseContext);
            InsertIntoCities(provinceId, "سیه‌رود", false, databaseContext);
            InsertIntoCities(provinceId, "شبستر", false, databaseContext);
            InsertIntoCities(provinceId, "شربیان", false, databaseContext);
            InsertIntoCities(provinceId, "شرفخانه", false, databaseContext);
            InsertIntoCities(provinceId, "شندآباد", false, databaseContext);
            InsertIntoCities(provinceId, "صوفیان", false, databaseContext);
            InsertIntoCities(provinceId, "عجب‌شیر", false, databaseContext);
            InsertIntoCities(provinceId, "قره‌آغاج", false, databaseContext);
            InsertIntoCities(provinceId, "کشک‌سرای", false, databaseContext);
            InsertIntoCities(provinceId, "کلوانق", false, databaseContext);
            InsertIntoCities(provinceId, "کلیبر", false, databaseContext);
            InsertIntoCities(provinceId, "کوزه‌کنان", false, databaseContext);
            InsertIntoCities(provinceId, "گوگان", false, databaseContext);
            InsertIntoCities(provinceId, "لیلان", false, databaseContext);
            InsertIntoCities(provinceId, "مراغه", false, databaseContext);
            InsertIntoCities(provinceId, "مرند", false, databaseContext);
            InsertIntoCities(provinceId, "ملکان", false, databaseContext);
            InsertIntoCities(provinceId, "ملک‌کیان", false, databaseContext);
            InsertIntoCities(provinceId, "ممقان", false, databaseContext);
            InsertIntoCities(provinceId, "مهربان", false, databaseContext);
            InsertIntoCities(provinceId, "میانه", false, databaseContext);
            InsertIntoCities(provinceId, "نظرکهریزی", false, databaseContext);
            InsertIntoCities(provinceId, "وایقان", false, databaseContext);
            InsertIntoCities(provinceId, "ورزقان", false, databaseContext);
            InsertIntoCities(provinceId, "هادی‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "هریس", false, databaseContext);
            InsertIntoCities(provinceId, "هشترود", false, databaseContext);
            InsertIntoCities(provinceId, "هوراند", false, databaseContext);
            InsertIntoCities(provinceId, "یامچی", false, databaseContext);

            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "آذربایجان غربی", databaseContext);
            InsertIntoCities(provinceId, "آواجیق", false, databaseContext);
            InsertIntoCities(provinceId, "ارومیه", true, databaseContext);
            InsertIntoCities(provinceId, "اشنویه", false, databaseContext);
            InsertIntoCities(provinceId, "ایواوغلی", false, databaseContext);
            InsertIntoCities(provinceId, "باروق", false, databaseContext);
            InsertIntoCities(provinceId, "بازرگان", false, databaseContext);
            InsertIntoCities(provinceId, "بوکان", false, databaseContext);
            InsertIntoCities(provinceId, "پلدشت", false, databaseContext);
            InsertIntoCities(provinceId, "پیرانشهر", false, databaseContext);
            InsertIntoCities(provinceId, "تازه‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "تکاب", false, databaseContext);
            InsertIntoCities(provinceId, "چهاربرج", false, databaseContext);
            InsertIntoCities(provinceId, "خوی", false, databaseContext);
            InsertIntoCities(provinceId, "ربط", false, databaseContext);
            InsertIntoCities(provinceId, "سردشت", false, databaseContext);
            InsertIntoCities(provinceId, "سرو", false, databaseContext);
            InsertIntoCities(provinceId, "سلماس", false, databaseContext);
            InsertIntoCities(provinceId, "سیلوانه", false, databaseContext);
            InsertIntoCities(provinceId, "سیمینه", false, databaseContext);
            InsertIntoCities(provinceId, "سیه‌چشمه", false, databaseContext);
            InsertIntoCities(provinceId, "شاهین‌دژ", false, databaseContext);
            InsertIntoCities(provinceId, "شوط", false, databaseContext);
            InsertIntoCities(provinceId, "فیرورق", false, databaseContext);
            InsertIntoCities(provinceId, "قره‌ضیاءالدین", false, databaseContext);
            InsertIntoCities(provinceId, "قوشچی", false, databaseContext);
            InsertIntoCities(provinceId, "کشاورز", false, databaseContext);
            InsertIntoCities(provinceId, "گردکشانه", false, databaseContext);
            InsertIntoCities(provinceId, "ماکو", false, databaseContext);
            InsertIntoCities(provinceId, "محمدیار", false, databaseContext);
            InsertIntoCities(provinceId, "محمودآباد", false, databaseContext);
            InsertIntoCities(provinceId, "مهاباد", false, databaseContext);
            InsertIntoCities(provinceId, "میاندوآب", false, databaseContext);
            InsertIntoCities(provinceId, "میرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "نالوس", false, databaseContext);
            InsertIntoCities(provinceId, "نقده", false, databaseContext);
            InsertIntoCities(provinceId, "نوشین‌شهر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "اردبیل", databaseContext);
            InsertIntoCities(provinceId, "آبی‌بیگلو", false, databaseContext);
            InsertIntoCities(provinceId, "اردبیل", true, databaseContext);
            InsertIntoCities(provinceId, "اصلاندوز", false, databaseContext);
            InsertIntoCities(provinceId, "بیله‌سوار", false, databaseContext);
            InsertIntoCities(provinceId, "پارس‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "تازه‌کند", false, databaseContext);
            InsertIntoCities(provinceId, "تازه‌کند انگوت", false, databaseContext);
            InsertIntoCities(provinceId, "جعفرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "خلخال", false, databaseContext);
            InsertIntoCities(provinceId, "رضی", false, databaseContext);
            InsertIntoCities(provinceId, "سرعین", false, databaseContext);
            InsertIntoCities(provinceId, "عنبران", false, databaseContext);
            InsertIntoCities(provinceId, "فخرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "کلور", false, databaseContext);
            InsertIntoCities(provinceId, "كوراييم", false, databaseContext);
            InsertIntoCities(provinceId, "گرمی", false, databaseContext);
            InsertIntoCities(provinceId, "گیوی", false, databaseContext);
            InsertIntoCities(provinceId, "لاهرود", false, databaseContext);
            InsertIntoCities(provinceId, "مشگین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "نمین", false, databaseContext);
            InsertIntoCities(provinceId, "نیر", false, databaseContext);
            InsertIntoCities(provinceId, "هشتجین", false, databaseContext);
            InsertIntoCities(provinceId, "هیر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "اصفهان", databaseContext);
            InsertIntoCities(provinceId, "ابریشم", false, databaseContext);
            InsertIntoCities(provinceId, "اردستان", false, databaseContext);
            InsertIntoCities(provinceId, "اژیه", false, databaseContext);
            InsertIntoCities(provinceId, "اصفهان", true, databaseContext);
            InsertIntoCities(provinceId, "افوس", false, databaseContext);
            InsertIntoCities(provinceId, "انارک", false, databaseContext);
            InsertIntoCities(provinceId, "ایمان‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "بادرود", false, databaseContext);
            InsertIntoCities(provinceId, "باغ بهادران", false, databaseContext);
            InsertIntoCities(provinceId, "برف‌انبار", false, databaseContext);
            InsertIntoCities(provinceId, "بهاران‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "بهارستان", false, databaseContext);
            InsertIntoCities(provinceId, "بویین و میاندشت", false, databaseContext);
            InsertIntoCities(provinceId, "پیربکران", false, databaseContext);
            InsertIntoCities(provinceId, "تودشک", false, databaseContext);
            InsertIntoCities(provinceId, "تیران", false, databaseContext);
            InsertIntoCities(provinceId, "جندق", false, databaseContext);
            InsertIntoCities(provinceId, "جوزدان", false, databaseContext);
            InsertIntoCities(provinceId, "چادگان", false, databaseContext);
            InsertIntoCities(provinceId, "چرمهین", false, databaseContext);
            InsertIntoCities(provinceId, "چم گردان", false, databaseContext);
            InsertIntoCities(provinceId, "حبیب‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "حسن‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "حنا", false, databaseContext);
            InsertIntoCities(provinceId, "خالدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "خمینی‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "خوانسار", false, databaseContext);
            InsertIntoCities(provinceId, "خور", false, databaseContext);
            InsertIntoCities(provinceId, "خوراسگان", false, databaseContext);
            InsertIntoCities(provinceId, "خورزوق", false, databaseContext);
            InsertIntoCities(provinceId, "داران", false, databaseContext);
            InsertIntoCities(provinceId, "دامنه", false, databaseContext);
            InsertIntoCities(provinceId, "درچه‌پیاز", false, databaseContext);
            InsertIntoCities(provinceId, "دستگرد", false, databaseContext);
            InsertIntoCities(provinceId, "دهاقان", false, databaseContext);
            InsertIntoCities(provinceId, "دهق", false, databaseContext);
            InsertIntoCities(provinceId, "دولت‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "دیزیچه", false, databaseContext);
            InsertIntoCities(provinceId, "رزوه", false, databaseContext);
            InsertIntoCities(provinceId, "رضوان‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "زاینده‌رود", false, databaseContext);
            InsertIntoCities(provinceId, "زرین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "زواره", false, databaseContext);
            InsertIntoCities(provinceId, "زیباشهر", false, databaseContext);
            InsertIntoCities(provinceId, "سده لنجان", false, databaseContext);
            InsertIntoCities(provinceId, "سگزی", false, databaseContext);
            InsertIntoCities(provinceId, "سمیرم", false, databaseContext);
            InsertIntoCities(provinceId, "شاهین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "شهرضا", false, databaseContext);
            InsertIntoCities(provinceId, "طالخونچه", false, databaseContext);
            InsertIntoCities(provinceId, "عسگران", false, databaseContext);
            InsertIntoCities(provinceId, "علویجه", false, databaseContext);
            InsertIntoCities(provinceId, "فریدون‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "فلاورجان", false, databaseContext);
            InsertIntoCities(provinceId, "فولادشهر", false, databaseContext);
            InsertIntoCities(provinceId, "قهدریجان", false, databaseContext);
            InsertIntoCities(provinceId, "كاشان", false, databaseContext);
            InsertIntoCities(provinceId, "کرکوند", false, databaseContext);
            InsertIntoCities(provinceId, "کلیشاد و سودرجان", false, databaseContext);
            InsertIntoCities(provinceId, "کمشجه", false, databaseContext);
            InsertIntoCities(provinceId, "کمه", false, databaseContext);
            InsertIntoCities(provinceId, "کهریزسنگ", false, databaseContext);
            InsertIntoCities(provinceId, "کوشک", false, databaseContext);
            InsertIntoCities(provinceId, "کوهپایه", false, databaseContext);
            InsertIntoCities(provinceId, "گز", false, databaseContext);
            InsertIntoCities(provinceId, "گلپایگان", false, databaseContext);
            InsertIntoCities(provinceId, "گل‌دشت", false, databaseContext);
            InsertIntoCities(provinceId, "گل‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "گوگد", false, databaseContext);
            InsertIntoCities(provinceId, "مبارکه", false, databaseContext);
            InsertIntoCities(provinceId, "محمدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "مشکات", false, databaseContext);
            InsertIntoCities(provinceId, "منظریه", false, databaseContext);
            InsertIntoCities(provinceId, "مهاباد", false, databaseContext);
            InsertIntoCities(provinceId, "میمه", false, databaseContext);
            InsertIntoCities(provinceId, "نایین", false, databaseContext);
            InsertIntoCities(provinceId, "نجف‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "نصرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "نطنز", false, databaseContext);
            InsertIntoCities(provinceId, "نیک‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "ورزنه", false, databaseContext);
            InsertIntoCities(provinceId, "ورنامخواست", false, databaseContext);
            InsertIntoCities(provinceId, "وزوان", false, databaseContext);
            InsertIntoCities(provinceId, "ونک", false, databaseContext);
            InsertIntoCities(provinceId, "هرند", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "ایلام", databaseContext);
            InsertIntoCities(provinceId, "آبدانان", false, databaseContext);
            InsertIntoCities(provinceId, "آسمان‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "ارکواز", false, databaseContext);
            InsertIntoCities(provinceId, "ایلام", true, databaseContext);
            InsertIntoCities(provinceId, "ایوان", false, databaseContext);
            InsertIntoCities(provinceId, "بدره", false, databaseContext);
            InsertIntoCities(provinceId, "پهله", false, databaseContext);
            InsertIntoCities(provinceId, "توحید", false, databaseContext);
            InsertIntoCities(provinceId, "چوار", false, databaseContext);
            InsertIntoCities(provinceId, "دره‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "دلگشا", false, databaseContext);
            InsertIntoCities(provinceId, "دهلران", false, databaseContext);
            InsertIntoCities(provinceId, "زرنه", false, databaseContext);
            InsertIntoCities(provinceId, "سرابله", false, databaseContext);
            InsertIntoCities(provinceId, "سراب‌باغ", false, databaseContext);
            InsertIntoCities(provinceId, "صالح‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "لومار", false, databaseContext);
            InsertIntoCities(provinceId, "مورموری", false, databaseContext);
            InsertIntoCities(provinceId, "موسیان", false, databaseContext);
            InsertIntoCities(provinceId, "مهران", false, databaseContext);
            InsertIntoCities(provinceId, "میمه", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "بوشهر", databaseContext);
            InsertIntoCities(provinceId, "آب‌پخش", false, databaseContext);
            InsertIntoCities(provinceId, "آبدان", false, databaseContext);
            InsertIntoCities(provinceId, "امام حسن", false, databaseContext);
            InsertIntoCities(provinceId, "اهرم", false, databaseContext);
            InsertIntoCities(provinceId, "برازجان", false, databaseContext);
            InsertIntoCities(provinceId, "بردخون", false, databaseContext);
            InsertIntoCities(provinceId, "بندر بوشهر", true, databaseContext);
            InsertIntoCities(provinceId, "بندر دیر", false, databaseContext);
            InsertIntoCities(provinceId, "بندر دیلم", false, databaseContext);
            InsertIntoCities(provinceId, "بندر ریگ", false, databaseContext);
            InsertIntoCities(provinceId, "بندر کنگان", false, databaseContext);
            InsertIntoCities(provinceId, "بندر گناوه", false, databaseContext);
            InsertIntoCities(provinceId, "تنگ ارم", false, databaseContext);
            InsertIntoCities(provinceId, "جم", false, databaseContext);
            InsertIntoCities(provinceId, "چغادک", false, databaseContext);
            InsertIntoCities(provinceId, "خارک", false, databaseContext);
            InsertIntoCities(provinceId, "خورموج", false, databaseContext);
            InsertIntoCities(provinceId, "دالکی", false, databaseContext);
            InsertIntoCities(provinceId, "دلوار", false, databaseContext);
            InsertIntoCities(provinceId, "ریز", false, databaseContext);
            InsertIntoCities(provinceId, "سعدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "شبانکاره", false, databaseContext);
            InsertIntoCities(provinceId, "طاهری", false, databaseContext);
            InsertIntoCities(provinceId, "عسلویه", false, databaseContext);
            InsertIntoCities(provinceId, "کاکی", false, databaseContext);
            InsertIntoCities(provinceId, "کلمه", false, databaseContext);
            InsertIntoCities(provinceId, "نخل تقی", false, databaseContext);
            InsertIntoCities(provinceId, "وحدتیه", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "تهران", databaseContext);
            InsertIntoCities(provinceId, "آبسرد", false, databaseContext);
            InsertIntoCities(provinceId, "آبعلی", false, databaseContext);
            InsertIntoCities(provinceId, "آسارا", false, databaseContext);
            InsertIntoCities(provinceId, "ارجمند", false, databaseContext);
            InsertIntoCities(provinceId, "اسلام‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "اشتهارد", false, databaseContext);
            InsertIntoCities(provinceId, "اندیشه", false, databaseContext);
            InsertIntoCities(provinceId, "باغستان", false, databaseContext);
            InsertIntoCities(provinceId, "باقرشهر", false, databaseContext);
            InsertIntoCities(provinceId, "بومهن", false, databaseContext);
            InsertIntoCities(provinceId, "پاکدشت", false, databaseContext);
            InsertIntoCities(provinceId, "پردیس", false, databaseContext);
            InsertIntoCities(provinceId, "پیشوا", false, databaseContext);
            InsertIntoCities(provinceId, "تجریش", false, databaseContext);
            InsertIntoCities(provinceId, "تهران", true, databaseContext);
            InsertIntoCities(provinceId, "جوادآباد", false, databaseContext);
            InsertIntoCities(provinceId, "چهارباغ", false, databaseContext);
            InsertIntoCities(provinceId, "چهاردانگه", false, databaseContext);
            InsertIntoCities(provinceId, "حسن‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "دماوند", false, databaseContext);
            InsertIntoCities(provinceId, "رباط‌کریم", false, databaseContext);
            InsertIntoCities(provinceId, "رودهن", false, databaseContext);
            InsertIntoCities(provinceId, "شاهدشهر", false, databaseContext);
            InsertIntoCities(provinceId, "شریف‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "شهر ری", false, databaseContext);
            InsertIntoCities(provinceId, "شهریار", false, databaseContext);
            InsertIntoCities(provinceId, "صالح‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "صباشهر", false, databaseContext);
            InsertIntoCities(provinceId, "صفادشت", false, databaseContext);
            InsertIntoCities(provinceId, "طالقان", false, databaseContext);
            InsertIntoCities(provinceId, "فردوسیه", false, databaseContext);
            InsertIntoCities(provinceId, "فشم", false, databaseContext);
            InsertIntoCities(provinceId, "فیروزکوه", false, databaseContext);
            InsertIntoCities(provinceId, "قدس", false, databaseContext);
            InsertIntoCities(provinceId, "قرچک", false, databaseContext);
            InsertIntoCities(provinceId, "کرج", false, databaseContext);
            InsertIntoCities(provinceId, "کمال‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "کوهسار", false, databaseContext);
            InsertIntoCities(provinceId, "کهریزک", false, databaseContext);
            InsertIntoCities(provinceId, "کیلان", false, databaseContext);
            InsertIntoCities(provinceId, "گرمدره", false, databaseContext);
            InsertIntoCities(provinceId, "گلستان", false, databaseContext);
            InsertIntoCities(provinceId, "لواسان", false, databaseContext);
            InsertIntoCities(provinceId, "ماهدشت", false, databaseContext);
            InsertIntoCities(provinceId, "محمدشهر", false, databaseContext);
            InsertIntoCities(provinceId, "مشکین‌دشت", false, databaseContext);
            InsertIntoCities(provinceId, "ملارد", false, databaseContext);
            InsertIntoCities(provinceId, "نسیم‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "نصیرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "نظرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "وحیدیه", false, databaseContext);
            InsertIntoCities(provinceId, "ورامین", false, databaseContext);
            InsertIntoCities(provinceId, "هشتگرد", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "چهار محال بختیاری", databaseContext);
            InsertIntoCities(provinceId, "آلونی", false, databaseContext);
            InsertIntoCities(provinceId, "اردل", false, databaseContext);
            InsertIntoCities(provinceId, "باباحیدر", false, databaseContext);
            InsertIntoCities(provinceId, "بروجن", false, databaseContext);
            InsertIntoCities(provinceId, "بلداجی", false, databaseContext);
            InsertIntoCities(provinceId, "بن", false, databaseContext);
            InsertIntoCities(provinceId, "جونقان", false, databaseContext);
            InsertIntoCities(provinceId, "چلگرد", false, databaseContext);
            InsertIntoCities(provinceId, "سامان", false, databaseContext);
            InsertIntoCities(provinceId, "سفیددشت", false, databaseContext);
            InsertIntoCities(provinceId, "سودجان", false, databaseContext);
            InsertIntoCities(provinceId, "سورشجان", false, databaseContext);
            InsertIntoCities(provinceId, "شلمزار", false, databaseContext);
            InsertIntoCities(provinceId, "شهرکرد", true, databaseContext);
            InsertIntoCities(provinceId, "طاقانک", false, databaseContext);
            InsertIntoCities(provinceId, "فارسان", false, databaseContext);
            InsertIntoCities(provinceId, "فرادنبه", false, databaseContext);
            InsertIntoCities(provinceId, "فرخ‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "کیان", false, databaseContext);
            InsertIntoCities(provinceId, "گندمان", false, databaseContext);
            InsertIntoCities(provinceId, "گهرو", false, databaseContext);
            InsertIntoCities(provinceId, "لردگان", false, databaseContext);
            InsertIntoCities(provinceId, "مال‌خلیفه", false, databaseContext);
            InsertIntoCities(provinceId, "ناغان", false, databaseContext);
            InsertIntoCities(provinceId, "نافچ", false, databaseContext);
            InsertIntoCities(provinceId, "نقنه", false, databaseContext);
            InsertIntoCities(provinceId, "هفشجان", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "خراسان جنوبی", databaseContext);
            InsertIntoCities(provinceId, "آرین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "آیسک", false, databaseContext);
            InsertIntoCities(provinceId, "اسدیه", false, databaseContext);
            InsertIntoCities(provinceId, "اسفدن", false, databaseContext);
            InsertIntoCities(provinceId, "اسلامیه", false, databaseContext);
            InsertIntoCities(provinceId, "بشرویه", false, databaseContext);
            InsertIntoCities(provinceId, "بیرجند", true, databaseContext);
            InsertIntoCities(provinceId, "حاجی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "خضری دشت بیاض", false, databaseContext);
            InsertIntoCities(provinceId, "خوسف", false, databaseContext);
            InsertIntoCities(provinceId, "زهان", false, databaseContext);
            InsertIntoCities(provinceId, "سرایان", false, databaseContext);
            InsertIntoCities(provinceId, "سربیشه", false, databaseContext);
            InsertIntoCities(provinceId, "سه‌قلعه", false, databaseContext);
            InsertIntoCities(provinceId, "شوسف", false, databaseContext);
            InsertIntoCities(provinceId, "طبس مسينا", false, databaseContext);
            InsertIntoCities(provinceId, "فردوس", false, databaseContext);
            InsertIntoCities(provinceId, "قائن", false, databaseContext);
            InsertIntoCities(provinceId, "قهستان", false, databaseContext);
            InsertIntoCities(provinceId, "مود", false, databaseContext);
            InsertIntoCities(provinceId, "نهبندان", false, databaseContext);
            InsertIntoCities(provinceId, "نیمبلوک", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "خراسان رضوی", databaseContext);
            InsertIntoCities(provinceId, "انابد", false, databaseContext);
            InsertIntoCities(provinceId, "باجگیران", false, databaseContext);
            InsertIntoCities(provinceId, "باخرز", false, databaseContext);
            InsertIntoCities(provinceId, "بایگ", false, databaseContext);
            InsertIntoCities(provinceId, "بجستان", false, databaseContext);
            InsertIntoCities(provinceId, "بردسکن", false, databaseContext);
            InsertIntoCities(provinceId, "بیدخت", false, databaseContext);
            InsertIntoCities(provinceId, "تایباد", false, databaseContext);
            InsertIntoCities(provinceId, "تربت جام", false, databaseContext);
            InsertIntoCities(provinceId, "تربت حیدریه", false, databaseContext);
            InsertIntoCities(provinceId, "جغتای", false, databaseContext);
            InsertIntoCities(provinceId, "چاپشلو", false, databaseContext);
            InsertIntoCities(provinceId, "چکنه", false, databaseContext);
            InsertIntoCities(provinceId, "چناران", false, databaseContext);
            InsertIntoCities(provinceId, "خرو", false, databaseContext);
            InsertIntoCities(provinceId, "خلیل‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "خواف", false, databaseContext);
            InsertIntoCities(provinceId, "داورزن", false, databaseContext);
            InsertIntoCities(provinceId, "دررود", false, databaseContext);
            InsertIntoCities(provinceId, "درگز", false, databaseContext);
            InsertIntoCities(provinceId, "دولت‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "رباط سنگ", false, databaseContext);
            InsertIntoCities(provinceId, "رشتخوار", false, databaseContext);
            InsertIntoCities(provinceId, "رضویه", false, databaseContext);
            InsertIntoCities(provinceId, "رودآب", false, databaseContext);
            InsertIntoCities(provinceId, "ریوش", false, databaseContext);
            InsertIntoCities(provinceId, "سبزوار", false, databaseContext);
            InsertIntoCities(provinceId, "سرخس", false, databaseContext);
            InsertIntoCities(provinceId, "سلطان‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "سنگان", false, databaseContext);
            InsertIntoCities(provinceId, "شاندیز", false, databaseContext);
            InsertIntoCities(provinceId, "ششتمد", false, databaseContext);
            InsertIntoCities(provinceId, "شهرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "صالح‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "طرقبه", false, databaseContext);
            InsertIntoCities(provinceId, "عشق‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فرهادگرد", false, databaseContext);
            InsertIntoCities(provinceId, "فریمان", false, databaseContext);
            InsertIntoCities(provinceId, "فیروزه", false, databaseContext);
            InsertIntoCities(provinceId, "فیض‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "قاسم‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "قدمگاه", false, databaseContext);
            InsertIntoCities(provinceId, "قلندرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "قوچان", false, databaseContext);
            InsertIntoCities(provinceId, "کاخک", false, databaseContext);
            InsertIntoCities(provinceId, "کاریز", false, databaseContext);
            InsertIntoCities(provinceId, "کاشمر", false, databaseContext);
            InsertIntoCities(provinceId, "کدکن", false, databaseContext);
            InsertIntoCities(provinceId, "کلات", false, databaseContext);
            InsertIntoCities(provinceId, "کندر", false, databaseContext);
            InsertIntoCities(provinceId, "گناباد", false, databaseContext);
            InsertIntoCities(provinceId, "لطف‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "مشهد", true, databaseContext);
            InsertIntoCities(provinceId, "مشهد ریزه", false, databaseContext);
            InsertIntoCities(provinceId, "ملک‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "نشتیفان", false, databaseContext);
            InsertIntoCities(provinceId, "نصرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "نقاب", false, databaseContext);
            InsertIntoCities(provinceId, "نوخندان", false, databaseContext);
            InsertIntoCities(provinceId, "نیشابور", false, databaseContext);
            InsertIntoCities(provinceId, "نیل‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "همت‌آباد", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "خراسان شمالی", databaseContext);
            InsertIntoCities(provinceId, "آشخانه", false, databaseContext);
            InsertIntoCities(provinceId, "اسفراین", false, databaseContext);
            InsertIntoCities(provinceId, "بجنورد", true, databaseContext);
            InsertIntoCities(provinceId, "پیش‌قلعه", false, databaseContext);
            InsertIntoCities(provinceId, "جاجرم", false, databaseContext);
            InsertIntoCities(provinceId, "حصار گرم‌خان", false, databaseContext);
            InsertIntoCities(provinceId, "درق", false, databaseContext);
            InsertIntoCities(provinceId, "راز", false, databaseContext);
            InsertIntoCities(provinceId, "سنخواست", false, databaseContext);
            InsertIntoCities(provinceId, "شوقان", false, databaseContext);
            InsertIntoCities(provinceId, "شیروان", false, databaseContext);
            InsertIntoCities(provinceId, "صفی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فاروج", false, databaseContext);
            InsertIntoCities(provinceId, "قاضی", false, databaseContext);
            InsertIntoCities(provinceId, "گرمه", false, databaseContext);
            InsertIntoCities(provinceId, "لوجلی", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "خوزستان", databaseContext);
            InsertIntoCities(provinceId, "آبادان", false, databaseContext);
            InsertIntoCities(provinceId, "آغاجاری", false, databaseContext);
            InsertIntoCities(provinceId, "اروندکنار", false, databaseContext);
            InsertIntoCities(provinceId, "الوان", false, databaseContext);
            InsertIntoCities(provinceId, "امیدیه", false, databaseContext);
            InsertIntoCities(provinceId, "اندیمشک", false, databaseContext);
            InsertIntoCities(provinceId, "اهواز", true, databaseContext);
            InsertIntoCities(provinceId, "ایذه", false, databaseContext);
            InsertIntoCities(provinceId, "باغ‌ملک", false, databaseContext);
            InsertIntoCities(provinceId, "بستان", false, databaseContext);
            InsertIntoCities(provinceId, "بندر امام خمینی", false, databaseContext);
            InsertIntoCities(provinceId, "بندر ماهشهر", false, databaseContext);
            InsertIntoCities(provinceId, "بهبهان", false, databaseContext);
            InsertIntoCities(provinceId, "جایزان", false, databaseContext);
            InsertIntoCities(provinceId, "چمران", false, databaseContext);
            InsertIntoCities(provinceId, "حر ریاحی", false, databaseContext);
            InsertIntoCities(provinceId, "حسینیه", false, databaseContext);
            InsertIntoCities(provinceId, "حمیدیه", false, databaseContext);
            InsertIntoCities(provinceId, "خرمشهر", false, databaseContext);
            InsertIntoCities(provinceId, "دزآب", false, databaseContext);
            InsertIntoCities(provinceId, "دزفول", false, databaseContext);
            InsertIntoCities(provinceId, "دهدز", false, databaseContext);
            InsertIntoCities(provinceId, "رامشیر", false, databaseContext);
            InsertIntoCities(provinceId, "رامهرمز", false, databaseContext);
            InsertIntoCities(provinceId, "رفیع", false, databaseContext);
            InsertIntoCities(provinceId, "زهره", false, databaseContext);
            InsertIntoCities(provinceId, "سالند", false, databaseContext);
            InsertIntoCities(provinceId, "سردشت", false, databaseContext);
            InsertIntoCities(provinceId, "سوسنگرد", false, databaseContext);
            InsertIntoCities(provinceId, "شادگان", false, databaseContext);
            InsertIntoCities(provinceId, "شوش", false, databaseContext);
            InsertIntoCities(provinceId, "شوشتر", false, databaseContext);
            InsertIntoCities(provinceId, "شیبان", false, databaseContext);
            InsertIntoCities(provinceId, "صفی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "صیدون", false, databaseContext);
            InsertIntoCities(provinceId, "قلعه خواجه", false, databaseContext);
            InsertIntoCities(provinceId, "قلعه‌تل", false, databaseContext);
            InsertIntoCities(provinceId, "گتوند", false, databaseContext);
            InsertIntoCities(provinceId, "لالی", false, databaseContext);
            InsertIntoCities(provinceId, "مسجدسلیمان", false, databaseContext);
            InsertIntoCities(provinceId, "مقاومت", false, databaseContext);
            InsertIntoCities(provinceId, "میانرود", false, databaseContext);
            InsertIntoCities(provinceId, "مینوشهر", false, databaseContext);
            InsertIntoCities(provinceId, "هفتگل", false, databaseContext);
            InsertIntoCities(provinceId, "هندیجان", false, databaseContext);
            InsertIntoCities(provinceId, "هویزه", false, databaseContext);
            InsertIntoCities(provinceId, "ویس", false, databaseContext);
            InsertIntoCities(provinceId, "ملاثانی", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "زنجان", databaseContext);
            InsertIntoCities(provinceId, "آب‌بر", false, databaseContext);
            InsertIntoCities(provinceId, "ابهر", false, databaseContext);
            InsertIntoCities(provinceId, "ارمغان‌خانه", false, databaseContext);
            InsertIntoCities(provinceId, "چورزق", false, databaseContext);
            InsertIntoCities(provinceId, "حلب", false, databaseContext);
            InsertIntoCities(provinceId, "خرمدره", false, databaseContext);
            InsertIntoCities(provinceId, "دندی", false, databaseContext);
            InsertIntoCities(provinceId, "زرین‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "زرین‌رود", false, databaseContext);
            InsertIntoCities(provinceId, "زنجان", true, databaseContext);
            InsertIntoCities(provinceId, "سجاس", false, databaseContext);
            InsertIntoCities(provinceId, "سلطانیه", false, databaseContext);
            InsertIntoCities(provinceId, "سهرورد", false, databaseContext);
            InsertIntoCities(provinceId, "صائین‌قلعه", false, databaseContext);
            InsertIntoCities(provinceId, "قیدار", false, databaseContext);
            InsertIntoCities(provinceId, "گرماب", false, databaseContext);
            InsertIntoCities(provinceId, "ماه‌نشان", false, databaseContext);
            InsertIntoCities(provinceId, "هیدج", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "سمنان", databaseContext);
            InsertIntoCities(provinceId, "آرادان", false, databaseContext);
            InsertIntoCities(provinceId, "امیریه", false, databaseContext);
            InsertIntoCities(provinceId, "ایوانکی", false, databaseContext);
            InsertIntoCities(provinceId, "بسطام", false, databaseContext);
            InsertIntoCities(provinceId, "بیارجمند", false, databaseContext);
            InsertIntoCities(provinceId, "دامغان", false, databaseContext);
            InsertIntoCities(provinceId, "درجزین", false, databaseContext);
            InsertIntoCities(provinceId, "دیباج", false, databaseContext);
            InsertIntoCities(provinceId, "سرخه", false, databaseContext);
            InsertIntoCities(provinceId, "سمنان", true, databaseContext);
            InsertIntoCities(provinceId, "شاهرود", false, databaseContext);
            InsertIntoCities(provinceId, "شهمیرزاد", false, databaseContext);
            InsertIntoCities(provinceId, "کلاته خیج", false, databaseContext);
            InsertIntoCities(provinceId, "گرمسار", false, databaseContext);
            InsertIntoCities(provinceId, "مجن", false, databaseContext);
            InsertIntoCities(provinceId, "مهدی‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "میامی", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "سیستان و بلوچستان", databaseContext);
            InsertIntoCities(provinceId, "ادیمی", false, databaseContext);
            InsertIntoCities(provinceId, "اسپکه", false, databaseContext);
            InsertIntoCities(provinceId, "ایرانشهر", false, databaseContext);
            InsertIntoCities(provinceId, "بزمان", false, databaseContext);
            InsertIntoCities(provinceId, "بمپور", false, databaseContext);
            InsertIntoCities(provinceId, "بنت", false, databaseContext);
            InsertIntoCities(provinceId, "بنجار", false, databaseContext);
            InsertIntoCities(provinceId, "پیشین", false, databaseContext);
            InsertIntoCities(provinceId, "جالق", false, databaseContext);
            InsertIntoCities(provinceId, "چابهار", false, databaseContext);
            InsertIntoCities(provinceId, "خاش", false, databaseContext);
            InsertIntoCities(provinceId, "دوست‌محمد", false, databaseContext);
            InsertIntoCities(provinceId, "راسک", false, databaseContext);
            InsertIntoCities(provinceId, "زابل", false, databaseContext);
            InsertIntoCities(provinceId, "زابلی", false, databaseContext);
            InsertIntoCities(provinceId, "زاهدان", true, databaseContext);
            InsertIntoCities(provinceId, "زهک", false, databaseContext);
            InsertIntoCities(provinceId, "سراوان", false, databaseContext);
            InsertIntoCities(provinceId, "سرباز", false, databaseContext);
            InsertIntoCities(provinceId, "سوران", false, databaseContext);
            InsertIntoCities(provinceId, "سیرکان", false, databaseContext);
            InsertIntoCities(provinceId, "فنوج", false, databaseContext);
            InsertIntoCities(provinceId, "قصرقند", false, databaseContext);
            InsertIntoCities(provinceId, "کنارک", false, databaseContext);
            InsertIntoCities(provinceId, "گلمورتی", false, databaseContext);
            InsertIntoCities(provinceId, "محمد‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "میرجاوه", false, databaseContext);
            InsertIntoCities(provinceId, "نصرت‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "نگور", false, databaseContext);
            InsertIntoCities(provinceId, "نوک‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "نیک‌شهر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "فارس", databaseContext);
            InsertIntoCities(provinceId, "آباده", false, databaseContext);
            InsertIntoCities(provinceId, "آباده طشک", false, databaseContext);
            InsertIntoCities(provinceId, "اردکان", false, databaseContext);
            InsertIntoCities(provinceId, "ارسنجان", false, databaseContext);
            InsertIntoCities(provinceId, "استهبان", false, databaseContext);
            InsertIntoCities(provinceId, "اسیر", false, databaseContext);
            InsertIntoCities(provinceId, "اشکنان", false, databaseContext);
            InsertIntoCities(provinceId, "افزر", false, databaseContext);
            InsertIntoCities(provinceId, "اقلید", false, databaseContext);
            InsertIntoCities(provinceId, "اهل", false, databaseContext);
            InsertIntoCities(provinceId, "اوز", false, databaseContext);
            InsertIntoCities(provinceId, "ایج", false, databaseContext);
            InsertIntoCities(provinceId, "ایزدخواست", false, databaseContext);
            InsertIntoCities(provinceId, "باب‌انار", false, databaseContext);
            InsertIntoCities(provinceId, "بالاده", false, databaseContext);
            InsertIntoCities(provinceId, "بنارویه", false, databaseContext);
            InsertIntoCities(provinceId, "بهمن", false, databaseContext);
            InsertIntoCities(provinceId, "بیرم", false, databaseContext);
            InsertIntoCities(provinceId, "بیضا", false, databaseContext);
            InsertIntoCities(provinceId, "جنت‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "جهرم", false, databaseContext);
            InsertIntoCities(provinceId, "جویم", false, databaseContext);
            InsertIntoCities(provinceId, "حاجی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "خاوران", false, databaseContext);
            InsertIntoCities(provinceId, "خرامه", false, databaseContext);
            InsertIntoCities(provinceId, "خشت", false, databaseContext);
            InsertIntoCities(provinceId, "خنج", false, databaseContext);
            InsertIntoCities(provinceId, "خور", false, databaseContext);
            InsertIntoCities(provinceId, "داراب", false, databaseContext);
            InsertIntoCities(provinceId, "داریان", false, databaseContext);
            InsertIntoCities(provinceId, "دهرم", false, databaseContext);
            InsertIntoCities(provinceId, "رامجرد", false, databaseContext);
            InsertIntoCities(provinceId, "رونیز", false, databaseContext);
            InsertIntoCities(provinceId, "زاهدشهر", false, databaseContext);
            InsertIntoCities(provinceId, "زرقان", false, databaseContext);
            InsertIntoCities(provinceId, "سده", false, databaseContext);
            InsertIntoCities(provinceId, "سروستان", false, databaseContext);
            InsertIntoCities(provinceId, "سعادت‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "سورمق", false, databaseContext);
            InsertIntoCities(provinceId, "سوریان", false, databaseContext);
            InsertIntoCities(provinceId, "سیدان", false, databaseContext);
            InsertIntoCities(provinceId, "ششده", false, databaseContext);
            InsertIntoCities(provinceId, "شهر پیر", false, databaseContext);
            InsertIntoCities(provinceId, "شیراز", true, databaseContext);
            InsertIntoCities(provinceId, "صغاد", false, databaseContext);
            InsertIntoCities(provinceId, "صفاشهر", false, databaseContext);
            InsertIntoCities(provinceId, "علامرودشت", false, databaseContext);
            InsertIntoCities(provinceId, "فتح‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فراشبند", false, databaseContext);
            InsertIntoCities(provinceId, "فسا", false, databaseContext);
            InsertIntoCities(provinceId, "فیروزآباد", false, databaseContext);
            InsertIntoCities(provinceId, "قائمیه", false, databaseContext);
            InsertIntoCities(provinceId, "قادرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "قطب‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "قیر", false, databaseContext);
            InsertIntoCities(provinceId, "کازرون", false, databaseContext);
            InsertIntoCities(provinceId, "کامفیروز", false, databaseContext);
            InsertIntoCities(provinceId, "کره‌ای", false, databaseContext);
            InsertIntoCities(provinceId, "کنارتخته", false, databaseContext);
            InsertIntoCities(provinceId, "کوار", false, databaseContext);
            InsertIntoCities(provinceId, "گراش", false, databaseContext);
            InsertIntoCities(provinceId, "گله‌دار", false, databaseContext);
            InsertIntoCities(provinceId, "لار", false, databaseContext);
            InsertIntoCities(provinceId, "لامرد", false, databaseContext);
            InsertIntoCities(provinceId, "لپویی", false, databaseContext);
            InsertIntoCities(provinceId, "لطیفی", false, databaseContext);
            InsertIntoCities(provinceId, "مرودشت", false, databaseContext);
            InsertIntoCities(provinceId, "مشکان", false, databaseContext);
            InsertIntoCities(provinceId, "مصیری", false, databaseContext);
            InsertIntoCities(provinceId, "مهر", false, databaseContext);
            InsertIntoCities(provinceId, "میمند", false, databaseContext);
            InsertIntoCities(provinceId, "نوجین", false, databaseContext);
            InsertIntoCities(provinceId, "نودان", false, databaseContext);
            InsertIntoCities(provinceId, "نورآباد", false, databaseContext);
            InsertIntoCities(provinceId, "نی‌ریز", false, databaseContext);
            InsertIntoCities(provinceId, "وراوی", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "قزوین", databaseContext);
            InsertIntoCities(provinceId, "آبگرم", false, databaseContext);
            InsertIntoCities(provinceId, "آبیک", false, databaseContext);
            InsertIntoCities(provinceId, "آوج", false, databaseContext);
            InsertIntoCities(provinceId, "ارداق", false, databaseContext);
            InsertIntoCities(provinceId, "اسفرورین", false, databaseContext);
            InsertIntoCities(provinceId, "اقبالیه", false, databaseContext);
            InsertIntoCities(provinceId, "الوند", false, databaseContext);
            InsertIntoCities(provinceId, "بویین‌زهرا", false, databaseContext);
            InsertIntoCities(provinceId, "بیدستان", false, databaseContext);
            InsertIntoCities(provinceId, "تاکستان", false, databaseContext);
            InsertIntoCities(provinceId, "خاکعلی", false, databaseContext);
            InsertIntoCities(provinceId, "خرمدشت", false, databaseContext);
            InsertIntoCities(provinceId, "دانسفهان", false, databaseContext);
            InsertIntoCities(provinceId, "رازمیان", false, databaseContext);
            InsertIntoCities(provinceId, "سگزآباد", false, databaseContext);
            InsertIntoCities(provinceId, "سیردان", false, databaseContext);
            InsertIntoCities(provinceId, "شال", false, databaseContext);
            InsertIntoCities(provinceId, "ضیاءآباد", false, databaseContext);
            InsertIntoCities(provinceId, "قزوین", true, databaseContext);
            InsertIntoCities(provinceId, "کوهین", false, databaseContext);
            InsertIntoCities(provinceId, "محمدیه", false, databaseContext);
            InsertIntoCities(provinceId, "محمودآباد نمونه", false, databaseContext);
            InsertIntoCities(provinceId, "معلم‌کلایه", false, databaseContext);
            InsertIntoCities(provinceId, "نرجه", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "قم", databaseContext);
            InsertIntoCities(provinceId, "جعفریه", false, databaseContext);
            InsertIntoCities(provinceId, "دستجرد", false, databaseContext);
            InsertIntoCities(provinceId, "سلفچگان", false, databaseContext);
            InsertIntoCities(provinceId, "قم", true, databaseContext);
            InsertIntoCities(provinceId, "قنوات", false, databaseContext);
            InsertIntoCities(provinceId, "کهک", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "کردستان", databaseContext);
            InsertIntoCities(provinceId, "آرمرده", false, databaseContext);
            InsertIntoCities(provinceId, "بابارشانی", false, databaseContext);
            InsertIntoCities(provinceId, "بانه", false, databaseContext);
            InsertIntoCities(provinceId, "بلبان‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "بویین سفلی", false, databaseContext);
            InsertIntoCities(provinceId, "بیجار", false, databaseContext);
            InsertIntoCities(provinceId, "چناره", false, databaseContext);
            InsertIntoCities(provinceId, "دزج", false, databaseContext);
            InsertIntoCities(provinceId, "دلبران", false, databaseContext);
            InsertIntoCities(provinceId, "دهگلان", false, databaseContext);
            InsertIntoCities(provinceId, "دیواندره", false, databaseContext);
            InsertIntoCities(provinceId, "زرینه", false, databaseContext);
            InsertIntoCities(provinceId, "سروآباد", false, databaseContext);
            InsertIntoCities(provinceId, "سریش‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "سقز", false, databaseContext);
            InsertIntoCities(provinceId, "سنندج", true, databaseContext);
            InsertIntoCities(provinceId, "شویشه", false, databaseContext);
            InsertIntoCities(provinceId, "صاحب", false, databaseContext);
            InsertIntoCities(provinceId, "قروه", false, databaseContext);
            InsertIntoCities(provinceId, "کامیاران", false, databaseContext);
            InsertIntoCities(provinceId, "کانی‌دینار", false, databaseContext);
            InsertIntoCities(provinceId, "کانی‌سور", false, databaseContext);
            InsertIntoCities(provinceId, "مریوان", false, databaseContext);
            InsertIntoCities(provinceId, "موچش", false, databaseContext);
            InsertIntoCities(provinceId, "یاسوکند", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "کرمان", databaseContext);
            InsertIntoCities(provinceId, "اختیارآباد", false, databaseContext);
            InsertIntoCities(provinceId, "ارزوئیه", false, databaseContext);
            InsertIntoCities(provinceId, "امین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "انار", false, databaseContext);
            InsertIntoCities(provinceId, "اندوهجرد", false, databaseContext);
            InsertIntoCities(provinceId, "باغین", false, databaseContext);
            InsertIntoCities(provinceId, "بافت", false, databaseContext);
            InsertIntoCities(provinceId, "بردسیر", false, databaseContext);
            InsertIntoCities(provinceId, "بروات", false, databaseContext);
            InsertIntoCities(provinceId, "بزنجان", false, databaseContext);
            InsertIntoCities(provinceId, "بم", false, databaseContext);
            InsertIntoCities(provinceId, "بهرمان", false, databaseContext);
            InsertIntoCities(provinceId, "پاریز", false, databaseContext);
            InsertIntoCities(provinceId, "جبالبارز", false, databaseContext);
            InsertIntoCities(provinceId, "جوزم", false, databaseContext);
            InsertIntoCities(provinceId, "جوپار", false, databaseContext);
            InsertIntoCities(provinceId, "جیرفت", false, databaseContext);
            InsertIntoCities(provinceId, "چترود", false, databaseContext);
            InsertIntoCities(provinceId, "خاتون‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "خانوک", false, databaseContext);
            InsertIntoCities(provinceId, "خرسند", false, databaseContext);
            InsertIntoCities(provinceId, "درب بهشت", false, databaseContext);
            InsertIntoCities(provinceId, "دهج", false, databaseContext);
            InsertIntoCities(provinceId, "رابر", false, databaseContext);
            InsertIntoCities(provinceId, "راور", false, databaseContext);
            InsertIntoCities(provinceId, "راین", false, databaseContext);
            InsertIntoCities(provinceId, "رفسنجان", false, databaseContext);
            InsertIntoCities(provinceId, "رودبار", false, databaseContext);
            InsertIntoCities(provinceId, "ریحان‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "زرند", false, databaseContext);
            InsertIntoCities(provinceId, "زنگی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "زیدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "سیرجان", false, databaseContext);
            InsertIntoCities(provinceId, "شهداد", false, databaseContext);
            InsertIntoCities(provinceId, "شهر بابک", false, databaseContext);
            InsertIntoCities(provinceId, "صفائیه", false, databaseContext);
            InsertIntoCities(provinceId, "عنبرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "فاریاب", false, databaseContext);
            InsertIntoCities(provinceId, "فهرج", false, databaseContext);
            InsertIntoCities(provinceId, "قلعه گنج", false, databaseContext);
            InsertIntoCities(provinceId, "کاظم‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "کرمان", true, databaseContext);
            InsertIntoCities(provinceId, "کشکوئیه", false, databaseContext);
            InsertIntoCities(provinceId, "کهنوج", false, databaseContext);
            InsertIntoCities(provinceId, "کوهبنان", false, databaseContext);
            InsertIntoCities(provinceId, "کیان‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "گلباف", false, databaseContext);
            InsertIntoCities(provinceId, "گلزار", false, databaseContext);
            InsertIntoCities(provinceId, "ماهان", false, databaseContext);
            InsertIntoCities(provinceId, "محمدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "محی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "مردهک", false, databaseContext);
            InsertIntoCities(provinceId, "مس سرچشمه", false, databaseContext);
            InsertIntoCities(provinceId, "منوجان", false, databaseContext);
            InsertIntoCities(provinceId, "نجف‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "نرماشیر", false, databaseContext);
            InsertIntoCities(provinceId, "نظام‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "نگار", false, databaseContext);
            InsertIntoCities(provinceId, "نودژ", false, databaseContext);
            InsertIntoCities(provinceId, "هجدک", false, databaseContext);
            InsertIntoCities(provinceId, "یزدان‌شهر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "کرمانشاه", databaseContext);
            InsertIntoCities(provinceId, "ازگله", false, databaseContext);
            InsertIntoCities(provinceId, "اسلام‌آباد غرب", false, databaseContext);
            InsertIntoCities(provinceId, "باینگان", false, databaseContext);
            InsertIntoCities(provinceId, "بیستون", false, databaseContext);
            InsertIntoCities(provinceId, "پاوه", false, databaseContext);
            InsertIntoCities(provinceId, "جوانرود", false, databaseContext);
            InsertIntoCities(provinceId, "حمیل", false, databaseContext);
            InsertIntoCities(provinceId, "رباط", false, databaseContext);
            InsertIntoCities(provinceId, "روانسر", false, databaseContext);
            InsertIntoCities(provinceId, "سرپل ذهاب", false, databaseContext);
            InsertIntoCities(provinceId, "سرمست", false, databaseContext);
            InsertIntoCities(provinceId, "سطر", false, databaseContext);
            InsertIntoCities(provinceId, "سنقر", false, databaseContext);
            InsertIntoCities(provinceId, "سومار", false, databaseContext);
            InsertIntoCities(provinceId, "صحنه", false, databaseContext);
            InsertIntoCities(provinceId, "قصر شیرین", false, databaseContext);
            InsertIntoCities(provinceId, "کرمانشاه", true, databaseContext);
            InsertIntoCities(provinceId, "کرند غرب", false, databaseContext);
            InsertIntoCities(provinceId, "کنگاور", false, databaseContext);
            InsertIntoCities(provinceId, "کوزران", false, databaseContext);
            InsertIntoCities(provinceId, "گهواره", false, databaseContext);
            InsertIntoCities(provinceId, "گیلان غرب", false, databaseContext);
            InsertIntoCities(provinceId, "میان‌راهان", false, databaseContext);
            InsertIntoCities(provinceId, "نودشه", false, databaseContext);
            InsertIntoCities(provinceId, "نوسود", false, databaseContext);
            InsertIntoCities(provinceId, "هرسین", false, databaseContext);
            InsertIntoCities(provinceId, "هلشی", false, databaseContext);
            InsertIntoCities(provinceId, "تازه‌آباد", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "کهکیلویه و بویر احمد", databaseContext);
            InsertIntoCities(provinceId, "باشت", false, databaseContext);
            InsertIntoCities(provinceId, "پاتاوه", false, databaseContext);
            InsertIntoCities(provinceId, "چرام", false, databaseContext);
            InsertIntoCities(provinceId, "چیتاب", false, databaseContext);
            InsertIntoCities(provinceId, "دهدشت", false, databaseContext);
            InsertIntoCities(provinceId, "دوگنبدان", false, databaseContext);
            InsertIntoCities(provinceId, "دیشموک", false, databaseContext);
            InsertIntoCities(provinceId, "سوق", false, databaseContext);
            InsertIntoCities(provinceId, "سی‌سخت", false, databaseContext);
            InsertIntoCities(provinceId, "قلعه رئیسی", false, databaseContext);
            InsertIntoCities(provinceId, "گراب سفلی", false, databaseContext);
            InsertIntoCities(provinceId, "لنده", false, databaseContext);
            InsertIntoCities(provinceId, "لیکک", false, databaseContext);
            InsertIntoCities(provinceId, "مارگون", false, databaseContext);
            InsertIntoCities(provinceId, "یاسوج", true, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "گلستان", databaseContext);
            InsertIntoCities(provinceId, "آزادشهر", false, databaseContext);
            InsertIntoCities(provinceId, "آق‌قلا", false, databaseContext);
            InsertIntoCities(provinceId, "انبار آلوم", false, databaseContext);
            InsertIntoCities(provinceId, "اینچه‌برون", false, databaseContext);
            InsertIntoCities(provinceId, "بندر ترکمن", false, databaseContext);
            InsertIntoCities(provinceId, "بندر گز", false, databaseContext);
            InsertIntoCities(provinceId, "خان‌ببین", false, databaseContext);
            InsertIntoCities(provinceId, "دلند", false, databaseContext);
            InsertIntoCities(provinceId, "رامیان", false, databaseContext);
            InsertIntoCities(provinceId, "سرخنکلاته", false, databaseContext);
            InsertIntoCities(provinceId, "سیمین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "علی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فاضل‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "کردکوی", false, databaseContext);
            InsertIntoCities(provinceId, "کلاله", false, databaseContext);
            InsertIntoCities(provinceId, "گالیکش", false, databaseContext);
            InsertIntoCities(provinceId, "گرگان", true, databaseContext);
            InsertIntoCities(provinceId, "گمیشان", false, databaseContext);
            InsertIntoCities(provinceId, "گنبد کاووس", false, databaseContext);
            InsertIntoCities(provinceId, "مراوه‌تپه", false, databaseContext);
            InsertIntoCities(provinceId, "مینودشت", false, databaseContext);
            InsertIntoCities(provinceId, "نگین‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "نوده خاندوز", false, databaseContext);
            InsertIntoCities(provinceId, "نوکنده", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "گیلان", databaseContext);
            InsertIntoCities(provinceId, "آستارا", false, databaseContext);
            InsertIntoCities(provinceId, "آستانه اشرفیه", false, databaseContext);
            InsertIntoCities(provinceId, "احمدسرگوراب", false, databaseContext);
            InsertIntoCities(provinceId, "اسالم", false, databaseContext);
            InsertIntoCities(provinceId, "اطاقور", false, databaseContext);
            InsertIntoCities(provinceId, "املش", false, databaseContext);
            InsertIntoCities(provinceId, "بازارجمعه", false, databaseContext);
            InsertIntoCities(provinceId, "بره‌سر", false, databaseContext);
            InsertIntoCities(provinceId, "بندر انزلی", false, databaseContext);
            InsertIntoCities(provinceId, "پره‌سر", false, databaseContext);
            InsertIntoCities(provinceId, "توتکابن", false, databaseContext);
            InsertIntoCities(provinceId, "جیرنده", false, databaseContext);
            InsertIntoCities(provinceId, "چابکسر", false, databaseContext);
            InsertIntoCities(provinceId, "چاف و چمخاله", false, databaseContext);
            InsertIntoCities(provinceId, "چوبر", false, databaseContext);
            InsertIntoCities(provinceId, "حویق", false, databaseContext);
            InsertIntoCities(provinceId, "خشکبیجار", false, databaseContext);
            InsertIntoCities(provinceId, "خمام", false, databaseContext);
            InsertIntoCities(provinceId, "دیلمان", false, databaseContext);
            InsertIntoCities(provinceId, "رانکوه", false, databaseContext);
            InsertIntoCities(provinceId, "رحیم‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "رستم‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "رشت", true, databaseContext);
            InsertIntoCities(provinceId, "رضوان‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "رودبار", false, databaseContext);
            InsertIntoCities(provinceId, "رودسر", false, databaseContext);
            InsertIntoCities(provinceId, "رودبنه", false, databaseContext);
            InsertIntoCities(provinceId, "سنگر", false, databaseContext);
            InsertIntoCities(provinceId, "سیاهکل", false, databaseContext);
            InsertIntoCities(provinceId, "شفت", false, databaseContext);
            InsertIntoCities(provinceId, "شلمان", false, databaseContext);
            InsertIntoCities(provinceId, "صومعه‌سرا", false, databaseContext);
            InsertIntoCities(provinceId, "فومن", false, databaseContext);
            InsertIntoCities(provinceId, "کلاچای", false, databaseContext);
            InsertIntoCities(provinceId, "کوچصفهان", false, databaseContext);
            InsertIntoCities(provinceId, "کومله", false, databaseContext);
            InsertIntoCities(provinceId, "کیاشهر", false, databaseContext);
            InsertIntoCities(provinceId, "گوراب زرمیخ", false, databaseContext);
            InsertIntoCities(provinceId, "لاهیجان", false, databaseContext);
            InsertIntoCities(provinceId, "لشت نشا", false, databaseContext);
            InsertIntoCities(provinceId, "لنگرود", false, databaseContext);
            InsertIntoCities(provinceId, "لوشان", false, databaseContext);
            InsertIntoCities(provinceId, "لوندویل", false, databaseContext);
            InsertIntoCities(provinceId, "لیسار", false, databaseContext);
            InsertIntoCities(provinceId, "ماسال", false, databaseContext);
            InsertIntoCities(provinceId, "ماسوله", false, databaseContext);
            InsertIntoCities(provinceId, "مرجغل", false, databaseContext);
            InsertIntoCities(provinceId, "منجیل", false, databaseContext);
            InsertIntoCities(provinceId, "واجارگاه", false, databaseContext);
            InsertIntoCities(provinceId, "هشتپر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "لرستان", databaseContext);
            InsertIntoCities(provinceId, "ازنا", false, databaseContext);
            InsertIntoCities(provinceId, "اشترینان", false, databaseContext);
            InsertIntoCities(provinceId, "الشتر", false, databaseContext);
            InsertIntoCities(provinceId, "الیگودرز", false, databaseContext);
            InsertIntoCities(provinceId, "بروجرد", false, databaseContext);
            InsertIntoCities(provinceId, "پل‌دختر", false, databaseContext);
            InsertIntoCities(provinceId, "چالانچولان", false, databaseContext);
            InsertIntoCities(provinceId, "چغلوندی", false, databaseContext);
            InsertIntoCities(provinceId, "چقابل", false, databaseContext);
            InsertIntoCities(provinceId, "خرم‌آباد", true, databaseContext);
            InsertIntoCities(provinceId, "درب گنبد", false, databaseContext);
            InsertIntoCities(provinceId, "دورود", false, databaseContext);
            InsertIntoCities(provinceId, "زاغه", false, databaseContext);
            InsertIntoCities(provinceId, "سپیددشت", false, databaseContext);
            InsertIntoCities(provinceId, "سراب‌دوره", false, databaseContext);
            InsertIntoCities(provinceId, "فیروزآباد", false, databaseContext);
            InsertIntoCities(provinceId, "کونانی", false, databaseContext);
            InsertIntoCities(provinceId, "کوهدشت", false, databaseContext);
            InsertIntoCities(provinceId, "گراب", false, databaseContext);
            InsertIntoCities(provinceId, "معمولان", false, databaseContext);
            InsertIntoCities(provinceId, "مومن‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "نورآباد", false, databaseContext);
            InsertIntoCities(provinceId, "ویسیان", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "مازندران", databaseContext);
            InsertIntoCities(provinceId, "آلاشت", false, databaseContext);
            InsertIntoCities(provinceId, "آمل", false, databaseContext);
            InsertIntoCities(provinceId, "امیرشهر", false, databaseContext);
            InsertIntoCities(provinceId, "ایزدشهر", false, databaseContext);
            InsertIntoCities(provinceId, "بابل", false, databaseContext);
            InsertIntoCities(provinceId, "بابلسر", false, databaseContext);
            InsertIntoCities(provinceId, "بلده", false, databaseContext);
            InsertIntoCities(provinceId, "بهشهر", false, databaseContext);
            InsertIntoCities(provinceId, "بهنمیر", false, databaseContext);
            InsertIntoCities(provinceId, "پل سفید", false, databaseContext);
            InsertIntoCities(provinceId, "تنکابن", false, databaseContext);
            InsertIntoCities(provinceId, "جویبار", false, databaseContext);
            InsertIntoCities(provinceId, "چالوس", false, databaseContext);
            InsertIntoCities(provinceId, "چمستان", false, databaseContext);
            InsertIntoCities(provinceId, "خرم‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "خلیل‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "خوش‌رودپی", false, databaseContext);
            InsertIntoCities(provinceId, "دابودشت", false, databaseContext);
            InsertIntoCities(provinceId, "رامسر", false, databaseContext);
            InsertIntoCities(provinceId, "رستمکلا", false, databaseContext);
            InsertIntoCities(provinceId, "رویان", false, databaseContext);
            InsertIntoCities(provinceId, "رینه", false, databaseContext);
            InsertIntoCities(provinceId, "زرگرمحله", false, databaseContext);
            InsertIntoCities(provinceId, "زیرآب", false, databaseContext);
            InsertIntoCities(provinceId, "ساری", true, databaseContext);
            InsertIntoCities(provinceId, "سرخ‌رود", false, databaseContext);
            InsertIntoCities(provinceId, "سلمان‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "سورک", false, databaseContext);
            InsertIntoCities(provinceId, "شیرگاه", false, databaseContext);
            InsertIntoCities(provinceId, "شیرود", false, databaseContext);
            InsertIntoCities(provinceId, "عباس‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فریدون‌کنار", false, databaseContext);
            InsertIntoCities(provinceId, "فریم", false, databaseContext);
            InsertIntoCities(provinceId, "قائم‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "کتالم و سادات‌شهر", false, databaseContext);
            InsertIntoCities(provinceId, "کلارآباد", false, databaseContext);
            InsertIntoCities(provinceId, "کلاردشت", false, databaseContext);
            InsertIntoCities(provinceId, "کله‌بست", false, databaseContext);
            InsertIntoCities(provinceId, "کوهی‌خیل", false, databaseContext);
            InsertIntoCities(provinceId, "کیاسر", false, databaseContext);
            InsertIntoCities(provinceId, "کیاکلا", false, databaseContext);
            InsertIntoCities(provinceId, "گزنک", false, databaseContext);
            InsertIntoCities(provinceId, "گلوگاه", false, databaseContext);
            InsertIntoCities(provinceId, "گلوگاه بابل", false, databaseContext);
            InsertIntoCities(provinceId, "گتاب", false, databaseContext);
            InsertIntoCities(provinceId, "محمودآباد", false, databaseContext);
            InsertIntoCities(provinceId, "مرزن‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "مرزیکلا", false, databaseContext);
            InsertIntoCities(provinceId, "نشتارود", false, databaseContext);
            InsertIntoCities(provinceId, "نکا", false, databaseContext);
            InsertIntoCities(provinceId, "نور", false, databaseContext);
            InsertIntoCities(provinceId, "نوشهر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "مرکزی", databaseContext);
            InsertIntoCities(provinceId, "آستانه", false, databaseContext);
            InsertIntoCities(provinceId, "آشتیان", false, databaseContext);
            InsertIntoCities(provinceId, "اراک", true, databaseContext);
            InsertIntoCities(provinceId, "پرندک", false, databaseContext);
            InsertIntoCities(provinceId, "تفرش", false, databaseContext);
            InsertIntoCities(provinceId, "توره", false, databaseContext);
            InsertIntoCities(provinceId, "خمین", false, databaseContext);
            InsertIntoCities(provinceId, "خنداب", false, databaseContext);
            InsertIntoCities(provinceId, "داودآباد", false, databaseContext);
            InsertIntoCities(provinceId, "دلیجان", false, databaseContext);
            InsertIntoCities(provinceId, "رازقان", false, databaseContext);
            InsertIntoCities(provinceId, "زاویه", false, databaseContext);
            InsertIntoCities(provinceId, "ساوه", false, databaseContext);
            InsertIntoCities(provinceId, "سنجان", false, databaseContext);
            InsertIntoCities(provinceId, "شازند", false, databaseContext);
            InsertIntoCities(provinceId, "غرق‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فرمهین", false, databaseContext);
            InsertIntoCities(provinceId, "قورچی‌باشی", false, databaseContext);
            InsertIntoCities(provinceId, "کرهرود", false, databaseContext);
            InsertIntoCities(provinceId, "کمیجان", false, databaseContext);
            InsertIntoCities(provinceId, "مأمونیه", false, databaseContext);
            InsertIntoCities(provinceId, "محلات", false, databaseContext);
            InsertIntoCities(provinceId, "میلاجرد", false, databaseContext);
            InsertIntoCities(provinceId, "نراق", false, databaseContext);
            InsertIntoCities(provinceId, "نوبران", false, databaseContext);
            InsertIntoCities(provinceId, "نیم‌ور", false, databaseContext);
            InsertIntoCities(provinceId, "هندودر", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "هرمزگان", databaseContext);
            InsertIntoCities(provinceId, "ابوموسی", false, databaseContext);
            InsertIntoCities(provinceId, "بستک", false, databaseContext);
            InsertIntoCities(provinceId, "بندر چارک", false, databaseContext);
            InsertIntoCities(provinceId, "بندر خمیر", false, databaseContext);
            InsertIntoCities(provinceId, "بندر عباس", true, databaseContext);
            InsertIntoCities(provinceId, "بندر لنگه", false, databaseContext);
            InsertIntoCities(provinceId, "پارسیان", false, databaseContext);
            InsertIntoCities(provinceId, "جاسک", false, databaseContext);
            InsertIntoCities(provinceId, "جناح", false, databaseContext);
            InsertIntoCities(provinceId, "حاجی‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "درگهان", false, databaseContext);
            InsertIntoCities(provinceId, "دهبارز", false, databaseContext);
            InsertIntoCities(provinceId, "رویدر", false, databaseContext);
            InsertIntoCities(provinceId, "زیارت‌علی", false, databaseContext);
            InsertIntoCities(provinceId, "سندرک", false, databaseContext);
            InsertIntoCities(provinceId, "سوزا", false, databaseContext);
            InsertIntoCities(provinceId, "سیریک", false, databaseContext);
            InsertIntoCities(provinceId, "فارغان", false, databaseContext);
            InsertIntoCities(provinceId, "فین", false, databaseContext);
            InsertIntoCities(provinceId, "قشم", false, databaseContext);
            InsertIntoCities(provinceId, "کنگ", false, databaseContext);
            InsertIntoCities(provinceId, "کیش", false, databaseContext);
            InsertIntoCities(provinceId, "هرمز", false, databaseContext);
            InsertIntoCities(provinceId, "هشت‌بندی", false, databaseContext);
            InsertIntoCities(provinceId, "میناب", false, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "همدان", databaseContext);
            InsertIntoCities(provinceId, "ازندریان", false, databaseContext);
            InsertIntoCities(provinceId, "اسدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "برزول", false, databaseContext);
            InsertIntoCities(provinceId, "بهار", false, databaseContext);
            InsertIntoCities(provinceId, "تویسرکان", false, databaseContext);
            InsertIntoCities(provinceId, "جورقان", false, databaseContext);
            InsertIntoCities(provinceId, "جوکار", false, databaseContext);
            InsertIntoCities(provinceId, "دمق", false, databaseContext);
            InsertIntoCities(provinceId, "رزن", false, databaseContext);
            InsertIntoCities(provinceId, "زنگنه", false, databaseContext);
            InsertIntoCities(provinceId, "سامن", false, databaseContext);
            InsertIntoCities(provinceId, "سرکان", false, databaseContext);
            InsertIntoCities(provinceId, "شیرین‌سو", false, databaseContext);
            InsertIntoCities(provinceId, "صالح‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "فامنین", false, databaseContext);
            InsertIntoCities(provinceId, "فرسفج", false, databaseContext);
            InsertIntoCities(provinceId, "فیروزان", false, databaseContext);
            InsertIntoCities(provinceId, "قروه درجزین", false, databaseContext);
            InsertIntoCities(provinceId, "قهاوند", false, databaseContext);
            InsertIntoCities(provinceId, "کبودرآهنگ", false, databaseContext);
            InsertIntoCities(provinceId, "گل‌تپه", false, databaseContext);
            InsertIntoCities(provinceId, "گیان", false, databaseContext);
            InsertIntoCities(provinceId, "لالجین", false, databaseContext);
            InsertIntoCities(provinceId, "مریانج", false, databaseContext);
            InsertIntoCities(provinceId, "ملایر", false, databaseContext);
            InsertIntoCities(provinceId, "مهاجران", false, databaseContext);
            InsertIntoCities(provinceId, "نهاوند", false, databaseContext);
            InsertIntoCities(provinceId, "همدان", true, databaseContext);
            provinceId = Guid.NewGuid();
            InsertInfoProvinces(provinceId, "یزد", databaseContext);
            InsertIntoCities(provinceId, "ابرکوه", false, databaseContext);
            InsertIntoCities(provinceId, "احمدآباد", false, databaseContext);
            InsertIntoCities(provinceId, "اردکان", false, databaseContext);
            InsertIntoCities(provinceId, "اشکذر", false, databaseContext);
            InsertIntoCities(provinceId, "بافق", false, databaseContext);
            InsertIntoCities(provinceId, "بهاباد", false, databaseContext);
            InsertIntoCities(provinceId, "تفت", false, databaseContext);
            InsertIntoCities(provinceId, "حمیدیا", false, databaseContext);
            InsertIntoCities(provinceId, "خضرآباد", false, databaseContext);
            InsertIntoCities(provinceId, "دیهوک", false, databaseContext);
            InsertIntoCities(provinceId, "زارچ", false, databaseContext);
            InsertIntoCities(provinceId, "شاهدیه", false, databaseContext);
            InsertIntoCities(provinceId, "طبس", false, databaseContext);
            InsertIntoCities(provinceId, "عشق‌آباد", false, databaseContext);
            InsertIntoCities(provinceId, "عقدا", false, databaseContext);
            InsertIntoCities(provinceId, "مروست", false, databaseContext);
            InsertIntoCities(provinceId, "مهردشت", false, databaseContext);
            InsertIntoCities(provinceId, "مهریز", false, databaseContext);
            InsertIntoCities(provinceId, "میبد", false, databaseContext);
            InsertIntoCities(provinceId, "ندوشن", false, databaseContext);
            InsertIntoCities(provinceId, "نیر", false, databaseContext);
            InsertIntoCities(provinceId, "هرات", false, databaseContext);
            InsertIntoCities(provinceId, "یزد", true, databaseContext);
        }

        public static void InsertInfoProvinces(Guid id, string title, DatabaseContext databaseContext)
        {
            Province province = new Province
            {
                Id = id,
                Title = title,
                IsActive = true,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                IsDeleted = false,
            };

            databaseContext.Provinces.Add(province);

        }

        public static void InsertIntoCities(Guid provinceId, string title, bool isCenter, DatabaseContext databaseContext)
        {
            City city = new City();

            if (title == "تهران")
            {
                city.Id = new Guid("2c730dce-774d-4007-88a9-4acb1dd48cea");
                city.ProvinceId = provinceId;
                city.Title = title;
                city.IsCenter = isCenter;
                city.IsActive = true;
                city.CreationDate = DateTime.Now;
                city.LastModifiedDate = DateTime.Now;
                city.IsDeleted = false;
            }
            else
            {
                city.ProvinceId = provinceId;
                city.Title = title;
                city.IsCenter = isCenter;
                city.IsActive = true;
                city.CreationDate = DateTime.Now;
                city.LastModifiedDate = DateTime.Now;
                city.IsDeleted = false;
            }

            databaseContext.Cities.Add(city);
        }

        public static void RoleSeed(DatabaseContext databaseContext)
        {
            InsertIntoRole(new Guid("e407f264d292400db3ad44675a1e4e97"), "راهبر", "Administrator", databaseContext);
            InsertIntoRole(new Guid("c0474d55c607498bbf0d219b244a9dc9"), "راهبر ویژه", "SuperAdministrator", databaseContext);
            InsertIntoRole(new Guid("0aeb583ae4e244d692aa39e7d2480127"), "مشتری", "Customer", databaseContext);
        }

        public static void InsertIntoRole(Guid id, string title, string name, DatabaseContext databaseContext)
        {
            Role role = new Role
            {
                Title = title,
                Name = name,
                IsActive = true,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                IsDeleted = false,
                Id = id,
            };

            databaseContext.Roles.Add(role);
        }


        public static void BaseUserSeed(DatabaseContext databaseContext)
        {
            Guid cityId = new Guid("2c730dce-774d-4007-88a9-4acb1dd48cea");

            InsertBaseUser(new Guid("307ebb90-9325-4991-b16c-eeb9b8e3e13e"), "09124806405", "123456", "hossein", "alibabaei",
                1000, cityId, new Guid("c0474d55c607498bbf0d219b244a9dc9"), databaseContext);


            InsertBaseUser(new Guid("87f69413-2e3e-4cb0-a44c-62215d970ee1"), "09124806404", "123456", "hossein", "alibabaei",
                1000, cityId, new Guid("0AEB583A-E4E2-44D6-92AA-39E7D2480127"), databaseContext);


            InsertBaseUser(new Guid("ba94d567-62d5-49aa-bebd-1277ac3e8703"), "09351252911", "123456", "siavash", "aghabalaii",
                1001, cityId, new Guid("0AEB583A-E4E2-44D6-92AA-39E7D2480127"), databaseContext);

        }
        public static void InsertBaseUser(Guid id, string cellNumber, string password, string firstName,
                    string lastName, int code, Guid city, Guid roleId, DatabaseContext databaseContext)
        {
            User user = new User()
            {
                CellNum = cellNumber,
                Username = cellNumber,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Code = code,
                CityId = city,
                RoleId = roleId,
                IsActive = true,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                IsDeleted = false,
                Id = id,
            };

            databaseContext.Users.Add(user);

        }
        
        public static void OrderStatusSeed(DatabaseContext databaseContext)
        {
            InsertIntoOrderStatus(new Guid("e4b8e0d4-ed24-4d47-8f0f-a9640193f2d7"), "ثبت سفارش", 1, databaseContext);
            InsertIntoOrderStatus(new Guid("babb5bb5-e1fb-44d3-99bd-a2c39fc63930"), "تایید سفارش", 2, databaseContext);
            InsertIntoOrderStatus(new Guid("3d3a706e-2deb-4913-bb15-2a31ede340d6"), "عدم تایید سفارش", 3, databaseContext);
            InsertIntoOrderStatus(new Guid("683c7779-9e02-43a7-a7ad-bfee3e2b9ee6"), "ارسال شده", 4, databaseContext);
            InsertIntoOrderStatus(new Guid("da161a32-8334-4b47-af71-e3d10c350833"), "تحویل شده", 5, databaseContext);
            InsertIntoOrderStatus(new Guid("d563eba9-dfb4-4ae6-aea6-8801cc37b0d4"), "لغو شده", 6, databaseContext);
        }

        public static void InsertIntoOrderStatus(Guid id, string title, int code, DatabaseContext databaseContext)
        {
            OrderStatus orderStatus = new OrderStatus
            {
                Title = title,
                Code = code,
                IsActive = true,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                IsDeleted = false,
                Id = id,
            };

            databaseContext.OrderStatuses.Add(orderStatus);
        }


        public static void ColorSeed(DatabaseContext databaseContext)
        {
            InsertInColor("الوان",null,"000", databaseContext);
            InsertInColor("مشکی", "181413", "001", databaseContext);
            InsertInColor("طوسی سیر", "7c7e7b", "004", databaseContext);
            InsertInColor("خاکی", "989699", "008", databaseContext);
            InsertInColor("سورمه ای", "4b577d", "017", databaseContext);
            InsertInColor("بنفش", "6d3945", "023", databaseContext);
            InsertInColor("صورتی", "806ace", "026", databaseContext);
            InsertInColor("سبز", "30bb7a", "034", databaseContext);
            InsertInColor("قرمز", "cf2008", "040", databaseContext);
            InsertInColor("زرشکی روشن", "", "048", databaseContext);
            InsertInColor("زرشکی ", "852938", "049", databaseContext);
            InsertInColor("کالباسی", "b7a5a4", "054", databaseContext);
            InsertInColor("کرم کاراملی", "e4e0dd", "055", databaseContext);
            InsertInColor("کاراملی متوسط", "", "057", databaseContext);

            InsertInColor("کاراملی تیره", "aea3a1", "058", databaseContext);
            InsertInColor("کرم ویزون", "ab948c", "059", databaseContext);
            InsertInColor("قهوه ای  جدید", "", "067", databaseContext);
            InsertInColor("جگری", "", "068", databaseContext);
            InsertInColor("سرخابی", "", "072", databaseContext);
            InsertInColor("زرد", "", "075", databaseContext);
            InsertInColor("قهوه ای 80 ", "6f442f", "080", databaseContext);

            InsertInColor("پوست پیازی", "c888a6", "084", databaseContext);
            InsertInColor("قهوه ای", "b7804f", "087", databaseContext);
            InsertInColor("قهوه ای تیره", "3d2217", "089", databaseContext);
            InsertInColor("استخوانی", "c0c0c0", "092", databaseContext);
            InsertInColor("عسلی مات", "c57d3f", "09", databaseContext);
            InsertInColor("زرد", "a07f38", "076", databaseContext);
            InsertInColor("پرتقالی", "dfa34f", "079", databaseContext);
            InsertInColor("فیروزه ای", "3fc9ed", "086", databaseContext);
            InsertInColor("طوسی ", "89877c", "07", databaseContext);
            InsertInColor("گردویی", "876c56", "062", databaseContext);
        }

        public static void InsertInColor(string title, string hexCode,string barCodeId, DatabaseContext databaseContext)
        {
            Color color = new Color
            {
                Title = title,
                HexCode = hexCode,
                IsActive = true,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                IsDeleted = false,
                Id = Guid.NewGuid(),
                BarCodeId = barCodeId
            };

            databaseContext.Colors.Add(color);
        }

        public static void SizeSeed(DatabaseContext databaseContext)
        {
            InsertInSize(38,58 , "c", databaseContext);
            InsertInSize(38, 48, "b", databaseContext);
            InsertInSize(48, 58, "k", databaseContext);
            InsertInSize(36, 45, "j", databaseContext);
            InsertInSize(48, 58, "m", databaseContext);
            InsertInSize(6, 13, "r", databaseContext);
        }


        public static void InsertInSize(int firstSize,int lastSize, string barCodeProductGroup, DatabaseContext databaseContext)
        {
            for (int i = firstSize; i <= lastSize; i++)
            {
                Size size = new Size
                {
                    Title = i.ToString(),
                    BarCodeProductGroup = barCodeProductGroup,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    IsDeleted = false,
                    Id = Guid.NewGuid(),
                };

                databaseContext.Sizes.Add(size);
            }


        }

    }
}
