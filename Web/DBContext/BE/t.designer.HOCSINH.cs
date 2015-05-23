using System;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
namespace t
{
	/// <summary>
	/// Generated Class for Table : HOCSINH.
	/// </summary>
	public partial class HOCSINH : TableBase
	{
		public HOCSINH() : base(){}

		/// <summary>
		/// Là View hay là Table.
		/// </summary>
		public override bool IsView 
		{
			get
			{
				return false;
			}
		}
		private int? m_HS_ID;
		/// <summary>
		/// ID học sinh:.
		/// </summary>
		public int? HS_ID
		{
			get
			{
				return m_HS_ID;
			}
			set
			{
				if ((this.m_HS_ID != value))
				{
					this.SendPropertyChanging("HS_ID");
					this.m_HS_ID = value;
					this.SendPropertyChanged("HS_ID");
				}
			}
		}

		private string m_HS_HOTEN;
		private bool m_HS_HOTENUpdated = false;
		/// <summary>
		/// Họ và tên:.
		/// </summary>
		public string HS_HOTEN
		{
			get
			{
				return m_HS_HOTEN;
			}
			set
			{
				if ((this.m_HS_HOTEN != value))
				{
					this.SendPropertyChanging("HS_HOTEN");
					this.m_HS_HOTEN = value;
					this.SendPropertyChanged("HS_HOTEN");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HS_HOTENUpdated = true;
				}
			}
		}

		private string m_HS_TUOI;
		private bool m_HS_TUOIUpdated = false;
		/// <summary>
		/// Tuổi:.
		/// </summary>
		public string HS_TUOI
		{
			get
			{
				return m_HS_TUOI;
			}
			set
			{
				if ((this.m_HS_TUOI != value))
				{
					this.SendPropertyChanging("HS_TUOI");
					this.m_HS_TUOI = value;
					this.SendPropertyChanged("HS_TUOI");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HS_TUOIUpdated = true;
				}
			}
		}

		private DateTime? m_HS_NGAYSINH;
		private bool m_HS_NGAYSINHUpdated = false;
		/// <summary>
		/// Ngày sinh:.
		/// </summary>
		public DateTime? HS_NGAYSINH
		{
			get
			{
				return m_HS_NGAYSINH;
			}
			set
			{
				if ((this.m_HS_NGAYSINH != value))
				{
					this.SendPropertyChanging("HS_NGAYSINH");
					this.m_HS_NGAYSINH = value;
					this.SendPropertyChanged("HS_NGAYSINH");
					if ((this.DataStatus != DBStatus.Inserted))
						this.m_HS_NGAYSINHUpdated = true;
				}
			}
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string Fields, string WhereClause, string OrderClause)
		{
			return "SELECT " + Fields + " FROM HOCSINH " + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause) + (string.IsNullOrEmpty(OrderClause) ? string.Empty : " ORDER BY " + OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause, string OrderClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(clsDAL.SelectField("[HS_ID]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HS_HOTEN]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HS_TUOI]", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("[HS_NGAYSINH]", ProType.DATETIME, this.DataManagement));
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(clsDAL.SelectField("HS_ID", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HS_HOTEN", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HS_TUOI", ProType.OTHER, this.DataManagement)).Append(",").Append(clsDAL.SelectField("HS_NGAYSINH", ProType.DATETIME, this.DataManagement));
				break;
			}
			return SelectStatement(sbSQL.ToString(), WhereClause, OrderClause);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu.
		/// </summary>
		public override string SelectStatement(string WhereClause)
		{
			return SelectStatement(WhereClause, string.Empty);
		}

		/// <summary>
		/// Tạo câu SQL lấy dữ liệu theo khóa chính.
		/// </summary>
		public override string SelectStatement()
		{
			return SelectStatement(WhereById());
		}

		/// <summary>
		/// Tạo câu SQL thêm dữ liệu.
		/// </summary>
		public override string InsertStatement()
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append("INSERT INTO HOCSINH ([HS_ID], [HS_HOTEN], [HS_TUOI], [HS_NGAYSINH]) VALUES(").Append("@HS_ID").Append(",").Append("@HS_HOTEN").Append(",").Append("@HS_TUOI").Append(",").Append("@HS_NGAYSINH").Append(")");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append("INSERT INTO HOCSINH (HS_ID, HS_HOTEN, HS_TUOI, HS_NGAYSINH) VALUES(").Append(":HS_ID").Append(",").Append(":HS_HOTEN").Append(",").Append(":HS_TUOI").Append(",").Append(":HS_NGAYSINH").Append(")");
				break;
			}
			return sbSQL.ToString();		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string Fields, string WhereClause)
		{
			return "UPDATE HOCSINH SET " + Fields + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
					return UpdateFullStatement(WhereClause);
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.Append(m_HS_HOTENUpdated ? string.Format(",[HS_HOTEN] = {0}", "@HS_HOTEN") : string.Empty).Append(m_HS_TUOIUpdated ? string.Format(",[HS_TUOI] = {0}", "@HS_TUOI") : string.Empty).Append(m_HS_NGAYSINHUpdated ? string.Format(",[HS_NGAYSINH] = {0}", "@HS_NGAYSINH") : string.Empty);
				break;
				 
				case DBManagement.Oracle:
				sbSQL.Append(m_HS_HOTENUpdated ? string.Format(",HS_HOTEN = {0}", ":HS_HOTEN") : string.Empty).Append(m_HS_TUOIUpdated ? string.Format(",HS_TUOI = {0}", ":HS_TUOI") : string.Empty).Append(m_HS_NGAYSINHUpdated ? string.Format(",HS_NGAYSINH = {0}", ":HS_NGAYSINH") : string.Empty);
				break;
			}
			if(sbSQL.Length > 0)
				return UpdateStatement(sbSQL.ToString().Substring(1), WhereClause);
			else
				return UpdateFullStatement(WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật dữ liệu.
		/// </summary>
		public override string UpdateFullStatement(string WhereClause)
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[HS_HOTEN] = {0}", "@HS_HOTEN").AppendFormat(",[HS_TUOI] = {0}", "@HS_TUOI").AppendFormat(",[HS_NGAYSINH] = {0}", "@HS_NGAYSINH");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HS_HOTEN = {0}", ":HS_HOTEN").AppendFormat(",HS_TUOI = {0}", ":HS_TUOI").AppendFormat(",HS_NGAYSINH = {0}", ":HS_NGAYSINH");
				break;
			}
			return UpdateStatement(sbSQL.ToString(), WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL cập nhật liêu theo khóa chính.
		/// </summary>
		public override string UpdateStatement()
		{
			return UpdateStatement(WhereById());
		}

		/// <summary>
		/// Tạo câu SQL xóa dữ liêu.
		/// </summary>
		public override string DeleteStatement(string WhereClause)
		{
			return clsDAL.DeleteString("HOCSINH", this.DataManagement) + (string.IsNullOrEmpty(WhereClause) ? string.Empty : " WHERE " + WhereClause);
		}

		/// <summary>
		/// Tạo câu SQL xóa dữ liệu theo khóa chính.
		/// </summary>
		public override string DeleteStatement()
		{
			 return DeleteStatement(WhereById());
		}

		/// <summary>
		/// Tạo điều kiện tìm kiếm theo khóa chính.
		/// </summary>
		public override string WhereById()
		{
			StringBuilder sbSQL = new StringBuilder();
			switch(this.DataManagement)
			{
				case DBManagement.Access:
				case DBManagement.SQL:
				case DBManagement.SQLLite:
				default:
				sbSQL.AppendFormat("[HS_ID] = {0}", "@HS_ID");
				break;
				 
				case DBManagement.Oracle:
				sbSQL.AppendFormat("HS_ID = {0}", ":HS_ID");
				break;
			}
			return sbSQL.ToString();
		}

		/// <summary>
		/// Tạo parameter để Delete dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> DeleteParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HS_ID", "Integer", clsDAL.ToDBParam(HS_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> UpdateParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HS_HOTEN", "WChar", clsDAL.ToDBParam(HS_HOTEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HS_TUOI", "WChar", clsDAL.ToDBParam(HS_TUOI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HS_NGAYSINH", "Date", clsDAL.ToDBParam(HS_NGAYSINH, ProType.DATETIME, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HS_ID", "Integer", clsDAL.ToDBParam(HS_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			return paramList;
		}

		/// <summary>
		/// Tạo parameter để Insert dữ liệu.
		/// </summary>
		public override List<IDbDataParameter> InsertParameters()
		{
			List<IDbDataParameter> paramList = new List<IDbDataParameter>();
			paramList.Add(clsDAL.CreateParameter("HS_ID", "Integer", clsDAL.ToDBParam(HS_ID, ProType.NUMBER, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HS_HOTEN", "WChar", clsDAL.ToDBParam(HS_HOTEN, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HS_TUOI", "WChar", clsDAL.ToDBParam(HS_TUOI, ProType.STRING, this.DataManagement) , this.DataManagement));
			paramList.Add(clsDAL.CreateParameter("HS_NGAYSINH", "Date", clsDAL.ToDBParam(HS_NGAYSINH, ProType.DATETIME, this.DataManagement), this.DataManagement));
			return paramList;
		}
	}
}