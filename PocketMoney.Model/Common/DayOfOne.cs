﻿using System.Runtime.Serialization;
using PocketMoney.Data;
using System;

namespace PocketMoney.Model
{
    [DataContract]
    public class DayOfOne : ObjectBase, IEquatable<DayOfOne>
    {
        public static int GetDayOfOneValue(DateTime date)
        {
            return new DayOfOne(date).Value;
        }

        protected DayOfOne() { }

        public DayOfOne(DateTime date)
            : this(Convert.ToByte(date.Year - 2000), Convert.ToByte(date.Month), Convert.ToByte(date.Day))
        {
        }

        public DayOfOne(byte year, byte month, byte day)
        {
            //1 2 4 8 16 32 64 128 256
            //1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17
            //1 2 3 4 5 6 1 2 3 4  1  2  3  4  5  6  7
            //|   days  | |month|  |       year      |

            this.Value = day | (month << 6) | (year << 10);
        }

        [Details]
        public int Value { get; set; }

        [DataMember]
        public int Year
        {
            get
            {
                return (this.Value >> 10) + 2000;
            }
        }

        [DataMember]
        public int Month
        {
            get
            {
                return (this.Value << 22) >> 28;
            }
        }

        [DataMember]
        public int Day
        {
            get
            {
                return (this.Value << 26) >> 26;
            }
        }

        public static bool operator ==(DayOfOne x, DayOfOne y)
        {
            return x.Value == y.Value;
        }

        public static bool operator !=(DayOfOne x, DayOfOne y)
        {
            return x.Value != y.Value;
        }

        public bool Equals(DayOfOne other)
        {
            return this.Value == other.Value;
        }
    }
}
