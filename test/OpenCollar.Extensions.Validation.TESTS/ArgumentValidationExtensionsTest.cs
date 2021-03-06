/*
 * This file is part of OpenCollar.Extensions.Validation.
 *
 * OpenCollar.Extensions.Validation is free software: you can redistribute it
 * and/or modify it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or (at your
 * option) any later version.
 *
 * OpenCollar.Extensions.Validation is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public
 * License for more details.
 *
 * You should have received a copy of the GNU General Public License along with
 * OpenCollar.Extensions.Validation.  If not, see <https://www.gnu.org/licenses/>.
 *
 * Copyright � 2019-2020 Jonathan Evans (jevans@open-collar.org.uk).
 */

using System;
using System.Collections.Generic;

using OpenCollar.Extensions.Validation;

using Xunit;

namespace OpenCollar.Extensions.Validation.TESTS.Validation
{
    public class ArgumentValidationExtensionsTest
    {
        /// <summary>
        ///     A test for ArgumentIsNotNullOrZeroLength (Array, string)
        /// </summary>
        [Fact]
        public void ArgumentIsNotNullOrZeroLengthTest()
        {
            new[] { 2 }.IsNotNullOrZeroLength("intArray");
        }

        /// <summary>
        ///     A test for ArgumentIsNotNullOrZeroLength (Array, string)
        /// </summary>
        [Fact]
        public void ArgumentIsNotNullOrZeroLengthTestIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new int[0].IsNotNullOrZeroLength("intArray"));
        }

        /// <summary>
        ///     A test for ArgumentIsNotNullOrZeroLength (Array, string)
        /// </summary>
        [Fact]
        public void ArgumentIsNotNullOrZeroLengthTestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ((int[])null).IsNotNullOrZeroLength("intArray"));
        }

        [Fact]
        public void TestEnumerableValidation()
        {
            IEnumerable<string> x = null;

            x.Validate("x", ObjectIs.None, ObjectIs.None);

            // ReSharper disable once HeuristicUnreachableCode
            Assert.Throws<ArgumentNullException>(() => x.Validate("x", ObjectIs.NotNull, ObjectIs.None));
            Assert.Throws<ArgumentNullException>(() => x.Validate(null, ObjectIs.NotNull, ObjectIs.None));
            Assert.Throws<ArgumentNullException>(() => x.Validate(string.Empty, ObjectIs.NotNull, ObjectIs.None));

            x = new string[] { "TEST1", "TEST2", null, "TEST4" };
            x.Validate("x", ObjectIs.None, ObjectIs.None);
            x.Validate("x", ObjectIs.NotNull, ObjectIs.None);
            x.Validate(null, ObjectIs.NotNull, ObjectIs.None);
            x.Validate(string.Empty, ObjectIs.NotNull, ObjectIs.None);

            var y = x.Validate("x", ObjectIs.NotNull, ObjectIs.None);
            Assert.Throws<ArgumentException>(() => y = x.Validate("x", ObjectIs.NotNull, ObjectIs.NotNull));
            Assert.Throws<ArgumentException>(() => y = x.Validate(null, ObjectIs.NotNull, ObjectIs.NotNull));
            y = ((string[])null).Validate("x", ObjectIs.None, ObjectIs.NotNull);
            Assert.NotNull(y);
            Assert.Empty(y);
        }

        [Fact]
        public void TestEnumValidation()
        {
            Assert.Throws<ArgumentException>(() => 1.Validate("y", EnumIs.ValidMember));

            var x = UriKind.Absolute;
            x.Validate("x", EnumIs.None);
            x.Validate("x", EnumIs.ValidMember);
            x.Validate("x", EnumIs.NonZero);
            x.Validate("x", EnumIs.NonZeroValidMember);
            Assert.Throws<ArgumentOutOfRangeException>(() => x.Validate("x", (EnumIs)666));

            x = UriKind.RelativeOrAbsolute; /* 0 */
            x.Validate("x", EnumIs.ValidMember);
            Assert.Throws<ArgumentOutOfRangeException>(() => x.Validate("x", EnumIs.NonZeroValidMember));
            Assert.Throws<ArgumentOutOfRangeException>(() => x.Validate("x", EnumIs.NonZero));

            x = (UriKind)666;
            Assert.Throws<ArgumentOutOfRangeException>(() => x.Validate("x", EnumIs.ValidMember));
            x.Validate("x", EnumIs.NonZero);
            Assert.Throws<ArgumentOutOfRangeException>(() => x.Validate("x", EnumIs.NonZeroValidMember));
        }

        [Fact]
        public void TestObjectValidation()
        {
            object x = null;
            x.Validate("x", ObjectIs.None);

            // ReSharper disable once HeuristicUnreachableCode
            Assert.Throws<ArgumentNullException>(() => x.Validate("x", ObjectIs.NotNull));
            Assert.Throws<ArgumentNullException>(() => x.Validate(null, ObjectIs.NotNull));
            Assert.Throws<ArgumentNullException>(() => x.Validate(string.Empty, ObjectIs.NotNull));
            x = new object();
            x.Validate("x", ObjectIs.None);
            x.Validate("x", ObjectIs.NotNull);
            x.Validate(null, ObjectIs.NotNull);
            x.Validate(string.Empty, ObjectIs.NotNull);
        }

        [Fact]
        public void TestRangeValidation_Decimal()
        {
            0.Validate("x", 0, 20);
            10.Validate("x", 0, 20);
            20.Validate("x", 0, 20);

            Assert.Throws<ArgumentOutOfRangeException>(() => (-1).Validate("x", 0, 20));
            Assert.Throws<ArgumentOutOfRangeException>(() => 21.Validate("x", 0, 20));
        }

        [Fact]
        public void TestRangeValidation_Double()
        {
            0D.Validate("x", 0D, 20D);
            10D.Validate("x", 0D, 20D);
            20D.Validate("x", 0D, 20D);

            Assert.Throws<ArgumentOutOfRangeException>(() => (-1D).Validate("x", 0D, 20D));
            Assert.Throws<ArgumentOutOfRangeException>(() => 21D.Validate("x", 0D, 20D));
        }

        [Fact]
        public void TestRangeValidation_Int32()
        {
            ((decimal)0).Validate("x", (decimal)0, (decimal)20);
            ((decimal)10).Validate("x", (decimal)0, (decimal)20);
            ((decimal)20).Validate("x", (decimal)0, (decimal)20);

            Assert.Throws<ArgumentOutOfRangeException>(() => ((decimal)-1).Validate("x", (decimal)0, (decimal)20));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((decimal)21).Validate("x", (decimal)0, (decimal)20));
        }

        [Theory]
        [InlineData("x")]
        [InlineData(null)]
        [InlineData("/r/n/t ")]
        [InlineData("")]
        public void TestStringValidation(string argumentName)
        {
            string x = null;

            // Check that errors can be thrown even if the argument name is null or empty.
            Assert.Throws<ArgumentNullException>(() => x.Validate(null, StringIs.NotNull));
            Assert.Throws<ArgumentNullException>(() => x.Validate(string.Empty, StringIs.NotNull));

            // Check that unknown doesn't throw in any circumstance.
            ((string)null).Validate(argumentName, StringIs.None);
            string.Empty.Validate(argumentName, StringIs.None);
            " \r\t\n".Validate(argumentName, StringIs.None);
            "TEST".Validate(argumentName, StringIs.None);

            // Check null validation
            Assert.Throws<ArgumentNullException>(() => x.Validate(argumentName, StringIs.NotNull));
            x.Validate(argumentName, StringIs.NotEmpty);

            // ReSharper disable once HeuristicUnreachableCode
            x.Validate(argumentName, StringIs.NotWhiteSpace);
            Assert.Throws<ArgumentNullException>(() => x.Validate(argumentName, StringIs.NotNullOrEmpty));
            Assert.Throws<ArgumentNullException>(() => x.Validate(argumentName, StringIs.NotNullOrWhiteSpace));
            x.Validate(argumentName, StringIs.NotEmptyOrWhiteSpace);
            Assert.Throws<ArgumentNullException>(() => x.Validate(argumentName, StringIs.NotNullEmptyOrWhiteSpace));

            // Check non-empty/null/whitespace strings
            x = "TEST";
            x.Validate(argumentName, StringIs.NotNull);
            x.Validate(argumentName, StringIs.NotEmpty);
            x.Validate(argumentName, StringIs.NotWhiteSpace);
            x.Validate(argumentName, StringIs.NotNullOrEmpty);
            x.Validate(argumentName, StringIs.NotNullOrWhiteSpace);
            x.Validate(argumentName, StringIs.NotEmptyOrWhiteSpace);
            x.Validate(argumentName, StringIs.NotNullEmptyOrWhiteSpace);

            // Check empty strings
            x = string.Empty;
            x.Validate(argumentName, StringIs.NotNull);
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotEmpty));
            x.Validate(argumentName, StringIs.NotWhiteSpace);
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotNullOrEmpty));
            x.Validate(argumentName, StringIs.NotNullOrWhiteSpace);
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotEmptyOrWhiteSpace));
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotNullEmptyOrWhiteSpace));

            // Check whitespace strings
            x = "\r\n\t ";
            x.Validate(argumentName, StringIs.NotNull);
            x.Validate(argumentName, StringIs.NotEmpty);
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotWhiteSpace));
            x.Validate(argumentName, StringIs.NotNullOrEmpty);
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotNullOrWhiteSpace));
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotEmptyOrWhiteSpace));
            Assert.Throws<ArgumentException>(() => x.Validate(argumentName, StringIs.NotNullEmptyOrWhiteSpace));
        }
    }
}