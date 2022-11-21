using System;
using UnityEngine;

namespace NumberData
{
    public struct NumberData
    {
        private const byte GRADE_SIZE = 10;
        private const byte DIMENSION_SIZE = 20;

        // value + grade
        private sbyte[] _numbers;

        public sbyte[] Numbers => _numbers;

        public NumberData(sbyte[] numbers)
        {
            _numbers = new sbyte[DIMENSION_SIZE];
            _numbers = numbers;
        }

        //TODO -> add postfix convert
        // like 10500 = 10.5 K
        public override string ToString()
        {
            string result = String.Empty;
            int zerosCount = 0;
            bool firstNotZeroFound = false;
            for (int i = DIMENSION_SIZE - 1; i >= 0; i--)
            {
                if (!firstNotZeroFound && _numbers[i] == 0)
                {
                    zerosCount++;
                    continue;
                }

                result += _numbers[i].ToString();
                firstNotZeroFound = true;
            }

            if (zerosCount == DIMENSION_SIZE)
            {
                //all zeros
                return "0";
            }

            return result;
        }

        private void Recalculate()
        {
            for (int i = 0; i < DIMENSION_SIZE; i++)
            {
                if (_numbers[i] >= GRADE_SIZE)
                {
                    var value = (sbyte)(_numbers[i] / GRADE_SIZE);
                    _numbers[i] -= (sbyte)(value * GRADE_SIZE);
                    _numbers[i + 1] += value;
                }

                if (_numbers[i] < 0)
                {
                    if (ContainsAnyPositiveDigit(_numbers, i))
                    {
                        _numbers[i] += (sbyte)GRADE_SIZE;
                        _numbers[i + 1] -= 1;
                    }
                    else
                    {
                        if (!IsLastIndexWithData(_numbers, i))
                        {
                            _numbers[i] *= -1;
                        }
                    }
                }
            }
        }

        private bool ContainsAnyPositiveDigit(sbyte[] arr, int index)
        {
            for (int i = index; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsLastIndexWithData(sbyte[] arr, int index)
        {
            for (int i = index + 1; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static NumberData operator +(NumberData a, NumberData b)
        {
            sbyte[] sum = new sbyte[DIMENSION_SIZE];
            int sign = 1;

            if (IsNegative(a) && IsNegative(b))
            {
                for (int i = 0; i < DIMENSION_SIZE; i++)
                {
                    sum[i] = (sbyte)(Math.Abs(a.Numbers[i]) + Math.Abs(b.Numbers[i]));
                }

                sign = -1;
            }
            else if (IsNegative(a) | IsNegative(b))
            {
                if (Abs(a) > Abs(b))
                {
                    for (int i = 0; i < DIMENSION_SIZE; i++)
                    {
                        sum[i] = (sbyte)(Math.Abs(a.Numbers[i]) - Math.Abs(b.Numbers[i]));
                    }

                    sign = Sign(a);
                }
                else
                {
                    for (int i = 0; i < DIMENSION_SIZE; i++)
                    {
                        sum[i] = (sbyte)(Math.Abs(b.Numbers[i]) - Math.Abs(a.Numbers[i]));
                    }

                    sign = Sign(b);
                }
            }
            else
            {
                for (int i = 0; i < DIMENSION_SIZE; i++)
                {
                    sum[i] = (sbyte)(a.Numbers[i] + b.Numbers[i]);
                }
            }

            var data = new NumberData(sum);
            data.Recalculate();
            data *= sign;


            return data;
        }

        public static NumberData operator -(NumberData a, NumberData b)
        {
            return a + (b * -1);
        }

        public static NumberData operator *(NumberData a, NumberData b)
        {
            sbyte[] mult = new sbyte[DIMENSION_SIZE];
            for (int i = 0; i < DIMENSION_SIZE; i++)
            {
                mult[i] = (sbyte)(a.Numbers[i] * b.Numbers[i]);
            }

            var data = new NumberData(mult);
            data.Recalculate();

            return new NumberData(mult);
        }

        public static NumberData operator *(NumberData a, int b)
        {
            sbyte[] mult = new sbyte[DIMENSION_SIZE];
            for (int i = 0; i < DIMENSION_SIZE; i++)
            {
                mult[i] = (sbyte)(a.Numbers[i] * b);
            }

            var data = new NumberData(mult);
            data.Recalculate();

            return new NumberData(mult);
        }

        public static bool operator >(NumberData a, NumberData b)
        {
            for (int i = DIMENSION_SIZE - 1; i >= 0; i--)
            {
                if (a.Numbers[i] > b.Numbers[i])
                {
                    return true;
                }

                if (a.Numbers[i] < b.Numbers[i])
                {
                    return false;
                }
            }

            return false;
        }
    
        public static bool operator >=(NumberData a, NumberData b)
        {
            for (int i = DIMENSION_SIZE - 1; i >= 0; i--)
            {
                if (a.Numbers[i] > b.Numbers[i])
                {
                    return true;
                }

                if (a.Numbers[i] < b.Numbers[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator <(NumberData a, NumberData b)
        {
            for (int i = DIMENSION_SIZE - 1; i >= 0; i--)
            {
                if (a.Numbers[i] > b.Numbers[i])
                {
                    return false;
                }

                if (a.Numbers[i] < b.Numbers[i])
                {
                    return true;
                }
            }

            return false;
        }
    
        public static bool operator <=(NumberData a, NumberData b)
        {
            for (int i = DIMENSION_SIZE - 1; i >= 0; i--)
            {
                if (a.Numbers[i] > b.Numbers[i])
                {
                    return false;
                }

                if (a.Numbers[i] < b.Numbers[i])
                {
                    return true;
                }
            }

            return true;
        }

        public static NumberData FromInt(int value)
        {
            sbyte[] numbers = new sbyte[DIMENSION_SIZE];
            int count = 0;
            var sign = Math.Sign(value);
            value = Math.Abs(value);
            while (value >= 1)
            {
                var k = value % GRADE_SIZE;
                numbers[count] = (sbyte)k;
                value /= GRADE_SIZE;
                count++;
            }

            if (count > 0)
            {
                numbers[count - 1] *= (sbyte)sign;
            }

            return new NumberData(numbers);
        }

        public static NumberData FromString(string str)
        {
            //TODO Validate
        
            sbyte[] numbers = new sbyte[DIMENSION_SIZE];
            if (str.Length > DIMENSION_SIZE)
            {
                Debug.LogError("string is too long! Maximum length is " + DIMENSION_SIZE);
                return new NumberData();
            }

            for (int i = 0; i < str.Length; i++)
            {
                string symbol = str[str.Length - 1 - i].ToString();
                if (symbol.Equals("-") && (i == str.Length - 1))
                {
                    numbers[i - 1] *= -1;
                    continue;
                }

                if (!sbyte.TryParse(symbol, out numbers[i]))
                {
                    Debug.LogErrorFormat("Cannot parse symbol {0} in string {1}", symbol, str);
                }
            }

            return new NumberData(numbers);
        }

        public static NumberData Abs(NumberData numberData)
        {
            var numbers = new sbyte[DIMENSION_SIZE];
            for (int i = 0; i < DIMENSION_SIZE; i++)
            {
                numbers[i] = Math.Abs(numberData.Numbers[i]);
            }

            return new NumberData(numbers);
        }

        public static sbyte Sign(NumberData numberData)
        {
            sbyte sign = 1;
            for (int i = 0; i < DIMENSION_SIZE; i++)
            {
                if (numberData.Numbers[i] < 0)
                {
                    sign = -1;
                }
            }

            return sign;
        }

        public static bool IsNegative(NumberData numberData)
        {
            return Sign(numberData) == -1;
        }
    }
}