using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.DatabaseСonverter
{
    public static class EncodingData
    {
        //преобразуйте растровое изображение в Base64String и храните его в SQLite
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        // Извлекает Base64String и преобразовать его снова Bitmap
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
