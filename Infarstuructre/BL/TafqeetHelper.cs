using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public static class TafqeetHelper
    {
        private static readonly string[] ArabicOnes =
        {
        "", "واحد", "اثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة",
        "أحد عشر", "اثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"
    };

        private static readonly string[] ArabicTens =
        {
        "", "", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون"
    };

        private static readonly string[] ArabicHundreds =
        {
        "", "مائة", "مئتان", "ثلاثمائة", "أربعمائة", "خمسمائة", "ستمائة", "سبعمائة", "ثمانمائة", "تسعمائة"
    };

        public static string ConvertToArabic(decimal amount)
        {
            if (amount == 0)
                return "صفر";

            var integerPart = (int)amount;
            var fractionalPart = (int)((amount - integerPart) * 100); // الأجزاء العشرية (قرش)

            // تحويل الجزء الصحيح
            var words = $"{ConvertToArabicWords(integerPart)} دينار";

            // تحويل الجزء العشري
            if (fractionalPart > 0)
            {
                var fractionalWords = ConvertFractionalPartToArabic(fractionalPart);
                words += $" و{fractionalWords} قرشًا";
            }

            // إضافة النص النهائي
            words += " فقط لا غير";

            return words;
        }

        private static string ConvertToArabicWords(int number)
        {
            if (number == 0)
                return "";

            if (number < 20)
                return ArabicOnes[number];

            if (number < 100)
            {
                if (number % 10 == 0) // معالجة الأعداد مثل 20, 30, 40
                    return ArabicTens[number / 10];
                return $"{ArabicTens[number / 10]} و{ArabicOnes[number % 10]}".Trim();
            }

            if (number < 1000)
                return $"{ArabicHundreds[number / 100]} {ConvertToArabicWords(number % 100)}".Trim();

            if (number < 1000000)
                return $"{ConvertToArabicWords(number / 1000)} ألف {ConvertToArabicWords(number % 1000)}".Trim();

            return $"{ConvertToArabicWords(number / 1000000)} مليون {ConvertToArabicWords(number % 1000000)}".Trim();
        }

        private static string ConvertFractionalPartToArabic(int fractionalPart)
        {
            int ones = fractionalPart % 10; // الآحاد
            int tens = fractionalPart / 10; // العشرات

            // إذا كانت العشرات صفر، نعرض الآحاد فقط
            if (tens == 0)
                return ArabicOnes[ones];

            // إذا كانت الآحاد صفر، نعرض العشرات فقط
            if (ones == 0)
                return ArabicTens[tens];

            // عرض الآحاد أولًا ثم العشرات
            return $"{ArabicOnes[ones]} و{ArabicTens[tens]}".Trim();
        }
    }
}
