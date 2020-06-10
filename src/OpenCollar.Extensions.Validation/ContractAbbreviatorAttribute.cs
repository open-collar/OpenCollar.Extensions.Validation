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
 * Copyright © 2019-2020 Jonathan Evans (jevans@open-collar.org.uk).
 */

using System;
using System.Diagnostics;

namespace OpenCollar.Extensions.Validation.Validation
{
    /// <summary>
    ///     Enables writing abbreviations for contracts that get copied to other methods
    /// </summary>
    /// <remarks>
    ///     Taken from this
    ///     <see href="http://geekswithblogs.net/terje/archive/2010/10/14/making-static-code-analysis-and-code-contracts-work-together-or.aspx">
    ///     Making Static Code Analysis and Code Contracts work together, or the CA1062 problem </see>
    /// </remarks>
    /// <remarks>
    ///     The following UML has been generated directly from the source code using
    ///     <a href="https://marketplace.visualstudio.com/items?itemName=jebbs.plantuml"> Jebbs PlantUML </a>. <img src="../images/uml-diagrams/Validation/ContractAbbreviatorAttribute/ContractAbbreviatorAttribute.svg" />
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractAbbreviatorAttribute : Attribute
    {
    }
}