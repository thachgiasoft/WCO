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
	/// DB context sử dụng trong using.
	/// </summary>
	public partial class tDBContext : IDisposable
	{
		private string m_ConnectionString = default(string);
		private IDbConnection m_Connection = null;
		private Dictionary<Type, object> m_Tables = default(Dictionary<Type,object>);
		private List<object> l_Tables = default(List<object>);
		/// <summary>
		/// Khởi tạo dữ liệu.
		/// </summary>
		private void Initialization()
		{
			m_Tables = new Dictionary<Type, object>();
			l_Tables = new List<object>();
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conn">Connection đến DB</param>
		public tDBContext(IDbConnection conn)
		{
			m_Connection = conn;
			m_ConnectionString = conn.ConnectionString;
			Initialization();
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conString">ConnectionString đến DB</param>
		public tDBContext(string conString)
			: this(conString, clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Hàm khởi tạo.
		/// </summary>
		/// <param name="conString">ConnectionString đến DB</param>
		public tDBContext(string conString, DBManagement DBMan)
			: this(clsConn.getConnection(conString, DBMan))
		{
			_dataManagement = DBMan;
		}

		/// <summary>
		/// Hàm khởi tạo. với connection mặc định
		/// </summary>
		public tDBContext()
			: this(clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Hàm khởi tạo. với connection mặc định
		/// </summary>
		public tDBContext(DBManagement DBMan)
			: this(clsConn.getConnection(DBMan))
		{
			_dataManagement = DBMan;
		}

		public void Dispose()
		{
			if (m_Connection != null && m_Connection.State == ConnectionState.Open)
				m_Connection.Close();
			m_Connection = null;
			if (m_Tables!=null)
				m_Tables.Clear();
			m_Tables = null;
		}

		private DBManagement _dataManagement;
		/// <summary>
		/// Hệ quản trị CSDL.
		/// </summary>
		public DBManagement DataManagement { get { return _dataManagement; } set { _dataManagement = value; } }
		/// <summary>
		/// Lấy ra Table kiểu T.
		/// </summary>
		protected DBTable<T> GetTable<T>()
		{
			if (!m_Tables.ContainsKey(typeof(T)))
			{
				DBTable<T> tbl = new DBTable<T>(m_Connection, this.DataManagement);
				l_Tables.Add(tbl);
				m_Tables.Add(typeof(T), tbl);
			}
			return m_Tables[typeof(T)] as DBTable<T>;
		}

		/// <summary>
		/// Lấy ra View kiểu T.
		/// </summary>
		protected DBView<T> GetView<T>()
		{
			if (!m_Tables.ContainsKey(typeof(T)))
			{
				m_Tables.Add(typeof(T), new DBView<T>(m_Connection, this.DataManagement));
			}
			return m_Tables[typeof(T)] as DBView<T>;
		}

		/// <summary>
		/// Thuộc tính đại diện cho bảng HOCSINH.
		/// </summary>
		public DBTable<HOCSINH> HOCSINHs
		{
			get
			{
				return GetTable<HOCSINH>();
			}
		}
		/// <summary>
		/// Cập nhật tất các thay đổi vào DB với transaction.
		/// </summary>
		/// <returns>Số dòng thay đổi</returns>
		public int SubmitAllChange()
		{
			int intReturn = 0;
			if (m_Connection == null) m_Connection = clsConn.getConnection(m_ConnectionString, this.DataManagement);
			if (m_Connection.State != ConnectionState.Open)
				m_Connection.Open();
			IDbTransaction trans = m_Connection.BeginTransaction();
			try
			{
				foreach (object table in l_Tables)
				{
					IDBTable tbl = table as IDBTable;
					if (tbl!=null)
						intReturn += tbl.SubmitAll(trans);
				}
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
			finally
			{
				m_Tables.Clear();
				l_Tables.Clear();
				m_Connection.Close();
			}
			return intReturn;
		}
	}
}