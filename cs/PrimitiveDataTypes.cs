using System;
using System.Collections.Generic;
using System.Text;

namespace cs {
    enum Color {
        Red, Green, Blue, Yellow, Cyan = 10, Magenta, White, Black
    };
    enum Orientation {
        Horizontal, Vertical
    };
    public class PrimitiveDataTypes {
        public static void DataTypesMain(String[] args) {
            byte b = 255;
            short s = 32767;
            int i = 2147483647;
            long l = 9223372036854775807;

            float f = 3.141f;
            double d = 3.141;
            decimal dec = 3.141M;

            String str = "hello";

            //Unsigned Int starts from 0 to that number
            uint ui = 4294967295;

            //pulls variable type from other type
            //can only be set once

            var x = f;

            //System.Console.WriteLine(x.GetType());
            Console.WriteLine(x);             // Don't need the System prefix

            Orientation o = Orientation.Horizontal;
        }
    }
}
