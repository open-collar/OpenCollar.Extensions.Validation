﻿/*
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
 * Copyright © 2019-2020 Jonathan Evans (jevans@open-collar.org.uk).
 */

using System;

namespace OpenCollar.Extensions.Validation
{
    /// <summary>
    ///     An enumeration defining the validation that can be applied to an enum value.
    /// </summary>
    /// <remarks>
    ///     The following UML has been generated directly from the source code using
    ///     <a href="https://marketplace.visualstudio.com/items?itemName=jebbs.plantuml"> Jebbs PlantUML </a>. <img src="../images/uml-diagrams/Validation/EnumIs/EnumIs.svg" />
    /// </remarks>
    [Flags]
    public enum EnumIs
    {
        /// <summary>
        ///     No validation is applied.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The value is any valid member of the enum.
        /// </summary>
        ValidMember = 1,

        /// <summary>
        ///     The value is any non-zero value (typically <c> Unknown </c> = 0).
        /// </summary>
        NonZero = 2,

        /// <summary>
        ///     The value is any valid, non-zero, member of the enum (typically <c> Unknown </c> = 0).
        /// </summary>
        NonZeroValidMember = ValidMember | NonZero
    }
}