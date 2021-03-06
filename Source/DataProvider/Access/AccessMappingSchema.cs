﻿using System;
using System.Text;

namespace LinqToDB.DataProvider.Access
{
	using Mapping;
	using SqlQuery;

	public class AccessMappingSchema : MappingSchema
	{
		public AccessMappingSchema() : this(ProviderName.Access)
		{
		}

		protected AccessMappingSchema(string configuration) : base(configuration)
		{
			SetDataType(typeof(DateTime), DataType.DateTime);

			SetValueToSqlConverter(typeof(bool),     (sb,dt,v) => sb.Append(v));
			SetValueToSqlConverter(typeof(Guid),     (sb,dt,v) => sb.Append("'").Append(((Guid)v).ToString("B")).Append("'"));
			SetValueToSqlConverter(typeof(DateTime), (sb,dt,v) => ConvertDateTimeToSql(sb, (DateTime)v));

			SetDataType(typeof(string), new SqlDataType(DataType.NVarChar, typeof(string), 255));
		}

		static void ConvertDateTimeToSql(StringBuilder stringBuilder, DateTime value)
		{
			var format = value.Hour == 0 && value.Minute == 0 && value.Second == 0 ?
				"#{0:yyyy-MM-dd}#" :
				"#{0:yyyy-MM-dd HH:mm:ss}#";

			stringBuilder.AppendFormat(format, value);
		}
	}
}
