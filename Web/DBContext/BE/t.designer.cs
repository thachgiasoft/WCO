using System;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
namespace t
{
	public enum DBStatus : int { Normal = 0, Inserted = 1, Updated = 2, Deleted = 3 }
	/// <summary>
	/// Class base dùng cho View
	/// </summary>
	public partial class ViewBase : ICloneable
	{
		/// <summary>
		/// Khởi tạo class.
		/// </summary>
		public ViewBase()
			:this(clsDAL.defaultDBMan)
		{
		}

		/// <summary>
		/// Khởi tạo class.
		/// </summary>
		public ViewBase(DBManagement DBMan)
		{
			_dataManagement = DBMan;
			_dataStatus = DBStatus.Inserted;
		}

		private DBManagement _dataManagement;
		/// <summary>
		/// Hệ quản trị CSDL.
		/// </summary>
		public DBManagement DataManagement { get { return _dataManagement; } set { _dataManagement = value; } }
		private DBStatus _dataStatus;
		/// <summary>
		/// Trạng thái của dữ liệu.
		/// </summary>
		public DBStatus DataStatus { get { return _dataStatus; } set { _dataStatus = value; } }
		private bool m_StopChangeEvent;
		/// <summary>
		/// Không gọi sự kiện change data.
		/// </summary>
		public bool StopChangeEvent { get { return m_StopChangeEvent; } set { m_StopChangeEvent = value; } }
		public virtual bool IsView { get { return false; } }
		public event PropertyChangingEventHandler PropertyChanging;
		/// <summary>
		/// Event khi chuẩn bị thay đổi dữ liệu.
		/// </summary>
		protected virtual void SendPropertyChanging(string propertyName)
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Event khi đã thay đổi dữ liệu.
		/// </summary>
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (!"DataStatus".Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))
			{
				if (this.DataStatus != DBStatus.Inserted)
					this.DataStatus = DBStatus.Updated;
			}
			if ((this.PropertyChanged != null) && !m_StopChangeEvent)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public virtual string SelectStatement(string Fields, string WhereClause, string OrderClause) { return string.Empty; }
		public virtual string SelectStatement(string WhereClause, string OrderClause) { return string.Empty; }
		public virtual string SelectStatement(string WhereClause) { return string.Empty; }
		public virtual string SelectStatement() { return string.Empty; }
		public object Clone()
		{
			return clsTranslator.TranslateObject(this.GetType(), this);
		}
	}

	/// <summary>
	/// Class base dùng cho Table
	/// </summary>
	public partial class TableBase : ViewBase
	{
		public TableBase()
			: base()
		{
		}

		public TableBase(DBManagement DBMan)
			: base(DBMan)
		{
		}

		public virtual string InsertStatement() { return string.Empty; }
		public virtual string UpdateStatement(string Fields, string WhereClause) { return string.Empty; }
		public virtual string UpdateStatement(string WhereClause) { return string.Empty; }
		public virtual string UpdateFullStatement(string WhereClause) { return string.Empty; }
		public virtual string UpdateStatement() { return string.Empty; }
		public virtual string DeleteStatement(string WhereClause) { return string.Empty; }
		public virtual string DeleteStatement() { return string.Empty; }
		public virtual string WhereById() { return string.Empty; }
		public virtual List<IDbDataParameter> InsertParameters() { return null; }
		public virtual List<IDbDataParameter> UpdateParameters() { return null; }
		public virtual List<IDbDataParameter> DeleteParameters() { return null; }
	}

	/// <summary>
	/// Interface dùng cho khi cập nhật dữ liệu.
	/// </summary>
	interface IDBTable
	{
		int SubmitAll(IDbTransaction trans); 
	}

	/// <summary>
	/// Class đại diện cho View
	/// </summary>
	public partial class DBView<T>
	{
		protected IDbConnection m_Connection = null;
		/// <summary>
		/// Khởi tạo với một connection
		/// </summary>
		public DBView(IDbConnection conn)
			: this(conn, clsDAL.defaultDBMan)
		{
		}

	/// <summary>
	/// Khởi tạo với một connection
	/// </summary>
		public DBView(IDbConnection conn, DBManagement DBMan)
		{
				_dataManagement = DBMan;
				m_Connection = conn;
		}

		private DBManagement _dataManagement;
		/// <summary>
		/// Hệ quản trị CSDL.
		/// </summary>
		public DBManagement DataManagement { get { return _dataManagement; } set { _dataManagement = value; } }
		/// <summary>
		/// Lấy ra một object kiểu T.
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// </summary>
		public T GetObject(string WhereClause)
		{
			return GetObject(WhereClause, m_Connection);
		}

		/// <summary>
		/// Lấy ra một object kiểu T.
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		/// </summary>
		public T GetObject(string WhereClause, IDbConnection conn)
		{
			T TObject = Activator.CreateInstance<T>();
			DataSet dsData = clsDAL.GetDataSet((TObject as ViewBase).SelectStatement(WhereClause), conn, this.DataManagement);
			if (dsData != null)
			{
				if (dsData.Tables[0].Rows.Count > 0)
				{
						clsAll.DataRow2Object(dsData.Tables[0].Rows[0], TObject);
						(TObject as ViewBase).DataStatus = DBStatus.Normal;
						(TObject as ViewBase).PropertyChanged += OnPropertyChanged_Handler;
						return TObject;
				}
			}
			return default(T);
		}

		/// <summary>
		/// Cập nhật trạng thái khi dữ liệu bị thay đổi.
		/// </summary>
		protected void OnPropertyChanged_Handler(object sender, PropertyChangedEventArgs e)
		{
		}

		/// <summary>
		/// Lấy giá trị của 1 cột.
		/// </summary>
		/// <param name="Fields">Định danh cột (ex:"ID, NAME")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="OrderClause">Sắp xếp theo (ex:"ID DESC")</param>
		public string GetValue(string Field, string WhereClause, string OrderClause)
		{
			ViewBase objBase = Activator.CreateInstance<T>() as ViewBase;
			return clsDAL.GetFValue(objBase.SelectStatement(Field, WhereClause, OrderClause), m_Connection, this.DataManagement);
		}

		/// <summary>
		/// Lấy ra một bảng dữ liệu với một dòng trống trên cùng.
		/// </summary>
		/// <param name="Fields">Định danh cột (ex:"ID, NAME")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="OrderClause">Sắp xếp theo (ex:"ID DESC")</param>
		public DataTable GetListChoCombobox(string Fields, string WhereClause, string OrderClause)
		{
			DataTable dt = GetList(Fields, WhereClause, OrderClause);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			return dt;
		}

		/// <summary>
		/// Lấy ra một bảng dữ liệu.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		public DataTable GetList(string WhereClause)
		{
			return GetList(string.Empty, WhereClause, string.Empty);
		}

		/// <summary>
		/// Lấy ra một bảng dữ liệu theo các cột được định danh.
		/// </summary>
		/// <param name="Fields">Định danh cột (ex:"ID, NAME")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		public DataTable GetList(string Fields, string WhereClause)
		{
			return GetList(Fields, WhereClause, string.Empty);
		}

		/// <summary>
		/// Lấy ra một bảng dữ liệu theo các cột được định danh.
		/// </summary>
		/// <param name="Fields">Định danh cột (ex:"ID, NAME")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="OrderClause">Sắp xếp theo (ex:"ID DESC")</param>
		public DataTable GetList(string Fields, string WhereClause, string OrderClause)
		{
			return GetList(Fields, WhereClause, OrderClause, m_Connection);
		}

		/// <summary>
		/// Lấy ra một bảng dữ liệu theo các cột được định danh.
		/// </summary>
		/// <param name="Fields">Định danh cột (ex:"ID, NAME")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="OrderClause">Sắp xếp theo (ex:"ID DESC")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public DataTable GetList(string Fields, string WhereClause, string OrderClause, IDbConnection conn)
		{
			DataSet dsData;
			ViewBase objBase = Activator.CreateInstance<T>() as ViewBase;
			if (!string.IsNullOrEmpty(Fields))
				dsData = clsDAL.GetDataSet(objBase.SelectStatement(Fields, WhereClause, OrderClause), conn, this.DataManagement);
			else
				dsData = clsDAL.GetDataSet(objBase.SelectStatement(WhereClause, OrderClause), conn, this.DataManagement);
			if (dsData != null)
			{
				return dsData.Tables[0];
			}
			return null;
		}

		/// <summary>
		/// Tạo một danh sách object kiểu T.
		/// </summary>
		/// <param name="dtData">DataTable chứa dữ liệu</param>
		protected List<T> CreateList(DataTable dtData)
		{
			List<T> list = new List<T>();
			if (dtData != null)
			{
				foreach (DataRow dr in dtData.Rows)
				{
					T obj = Activator.CreateInstance<T>();
					clsAll.DataRow2Object(dr, obj);
					(obj as ViewBase).DataStatus = DBStatus.Normal;
					(obj as ViewBase).PropertyChanged+= OnPropertyChanged_Handler;
					list.Add(obj);
				}
			}
			return list;
		}

		/// <summary>
		/// Lấy ra một danh sách object kiểu T.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="OrderClause">Sắp xếp theo (ex:"ID DESC")</param>
		public List<T> GetListObject(string WhereClause, string OrderClause)
		{
			return GetListObject(WhereClause, OrderClause, m_Connection);
		}

		/// <summary>
		/// Lấy ra một danh sách object kiểu T.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="OrderClause">Sắp xếp theo (ex:"ID DESC")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public List<T> GetListObject(string WhereClause, string OrderClause, IDbConnection conn)
		{
			return CreateList(GetList(string.Empty, WhereClause, OrderClause, conn));
		}

		/// <summary>
		/// Lấy ra một danh sách object kiểu T.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		public List<T> GetListObject(string WhereClause)
		{
			return GetListObject(WhereClause, string.Empty, m_Connection);
		}

		/// <summary>
		/// Lấy ra một danh sách object kiểu T.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn lấy dữ liệu (ex:"ID = 1")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public List<T> GetListObject(string WhereClause, IDbConnection conn)
		{
			return GetListObject(WhereClause, string.Empty, conn);
		}

		/// <summary>
		/// Đếm số dòng dữ liệu trong bảng.
		/// </summary>
		public int Count()
		{
			return Count("1 = 1");
		}

		/// <summary>
		/// Đếm số dòng dữ liệu trong bảng.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn đếm (ex:"ID = 1")</param>
		public int Count(string WhereClause)
		{
			return Count(WhereClause, m_Connection);
		}

		/// <summary>
		/// Đếm số dòng dữ liệu trong bảng.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn đếm (ex:"ID = 1")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public int Count(string WhereClause, IDbConnection conn)
		{
			ViewBase objBase = Activator.CreateInstance<T>() as ViewBase;
			return int.Parse(clsDAL.GetFValue(objBase.SelectStatement("COUNT(*) AS CNT", WhereClause, string.Empty), conn, this.DataManagement));
		}

		/// <summary>
		/// Kiểm tra sự tồn tai của dữ liệu dữ liệu trong bảng.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn kiểm tra (ex:"ID = 1")</param>
		public bool Exist(string WhereClause)
		{
			return Exist(WhereClause, m_Connection);
		}

		/// <summary>
		/// Kiểm tra sự tồn tai của dữ liệu dữ liệu trong bảng.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn kiểm tra (ex:"ID = 1")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public bool Exist(string WhereClause, IDbConnection conn)
		{
			return Count(WhereClause, conn) > 0;
		}

		/// <summary>
		/// Lấy giá trị lớn nhất của 1 trường trong bảng.
		/// </summary>
		/// <param name="Field">Định danh cột (ex:"ID")</param>
		public string Max(string Field)
		{
			return Max(Field, string.Empty);
		}

		/// <summary>
		/// Lấy giá trị lớn nhất của 1 trường trong bảng.
		/// </summary>
		/// <param name="Field">Định danh cột (ex:"ID")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy (ex:"ID = 1")</param>
		public string Max(string Field, string WhereClause)
		{
			return Max(Field, WhereClause, m_Connection);
		}

		/// <summary>
		/// Lấy giá trị lớn nhất của 1 trường trong bảng.
		/// </summary>
		/// <param name="Field">Định danh cột (ex:"ID")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy (ex:"ID = 1")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public string Max(string Field, string WhereClause, IDbConnection conn)
		{
			ViewBase objBase = Activator.CreateInstance<T>() as ViewBase;
			switch (this.DataManagement)
			{
				case DBManagement.Oracle:
					return clsDAL.GetFValue((objBase.SelectStatement(string.Format("{0}", Field), string.IsNullOrEmpty(WhereClause) ? "ROWNUM = 1" : WhereClause + "AND ROWNUM = 1", string.Format("{0} DESC", Field))), conn, this.DataManagement);
				default:
					return clsDAL.GetFValue((objBase.SelectStatement(string.Format("TOP 1 {0}", Field), WhereClause, string.Format("{0} DESC", Field))), conn, this.DataManagement);
			}

		}

		/// <summary>
		/// Lấy giá trị nhỏ nhất của 1 trường trong bảng.
		/// </summary>
		/// <param name="Field">Định danh cột (ex:"ID")</param>
		public string Min(string Field)
		{
			return Min(Field, string.Empty);
		}

		/// <summary>
		/// Lấy giá trị nhỏ nhất của 1 trường trong bảng.
		/// </summary>
		/// <param name="Field">Định danh cột (ex:"ID")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy (ex:"ID = 1")</param>
		public string Min(string Field, string WhereClause)
		{
			return Min(Field, WhereClause, m_Connection);
		}

		/// <summary>
		/// Lấy giá trị nhỏ nhất của 1 trường trong bảng.
		/// </summary>
		/// <param name="Field">Định danh cột (ex:"ID")</param>
		/// <param name="WhereClause">Điều kiện muốn lấy (ex:"ID = 1")</param>
		/// <param name="conn">Connection kết nối để lấy dữ liệu</param>
		public string Min(string Field, string WhereClause, IDbConnection conn)
		{
			ViewBase objBase = Activator.CreateInstance<T>() as ViewBase;
			return clsDAL.GetFValue((objBase.SelectStatement(string.Format("TOP 1 {0}", Field), WhereClause, string.Format("{0} ASC", Field))), conn, this.DataManagement);
		}
	}

	/// <summary>
	///Class đại diện cho Table
	/// </summary>
	public partial class DBTable<T> : DBView<T>, IDBTable
	{
		private List<T> Inserteds;
		private List<T> Updateds;
		private List<T> Deleteds;
		private List<string> UpdatedSQLs;
		private List<string> DeletedSQLs;
		/// <summary>
		/// Khởi tạo tạo danh sách dữ liệu.
		/// </summary>
		protected void OnCreated()
		{
			Inserteds = new List<T>();
			Updateds = new List<T>();
			Deleteds = new List<T>();
			UpdatedSQLs = new List<string>();
			DeletedSQLs = new List<string>();
		}

		public DBTable(IDbConnection conn)
			: this(conn, clsDAL.defaultDBMan)
		{
		}

		public DBTable(IDbConnection conn, DBManagement DBMan)
			: base(conn, DBMan)
		{
				OnCreated();
		}

		/// <summary>
		/// Thêm một dòng vào bảng.
		/// </summary>
		/// <param name="TObject">Object kiểu T muốn thêm vào</param>
		public void InsertOnSubmit(T TObject)
		{
			if (!Inserteds.Contains(TObject))
			{
				Inserteds.Add(TObject);
			}
		}

		/// <summary>
		/// Thêm một danh sách dòng vào bảng.
		/// </summary>
		/// <param name="TObjects">Danh sách object kiểu T muốn thêm vào</param>
		public void InsertAllOnSubmit(IEnumerable<T> TObjects)
		{
			foreach (T TObject in TObjects)
			{
				if (!Inserteds.Contains(TObject))
				{
					Inserteds.Add(TObject);
				}
			}
		}

		/// <summary>
		/// Cập nhật một dòng vào bảng.
		/// </summary>
		/// <param name="TObject">Object kiểu T muốn cập nhật</param>
		public void UpdateOnSubmit(T TObject)
		{
			if (!Updateds.Contains(TObject))
			{
				Updateds.Add(TObject);
			}
		}

		/// <summary>
		/// Cập nhật tất cả các dòng trong bảng.
		/// </summary>
		/// <param name="Fields">Các trường muốn cập nhật (ex: ID = 1, NAME = 'ABC')</param>
		public void UpdateOnSubmit(string Fields)
		{
			UpdateOnSubmit(Fields, "1 = 1");
		}

		/// <summary>
		/// Cập nhật tất cả các dòng trong bảng.
		/// </summary>
		/// <param name="Fields">Các trường muốn cập nhật (ex: ID = 1, NAME = 'ABC')</param>
		/// <param name="WhereClause">Điều kiện muốn cập nhật (ex: ID = 1 AND NAME = 'ABC')</param>
		public void UpdateOnSubmit(string Fields, string WhereClause)
		{
			TableBase TObject = Activator.CreateInstance<T>() as TableBase;
			if (TObject != null)
			{
				UpdatedSQLs.Add(TObject.UpdateStatement(Fields, WhereClause));
			}
		}

		/// <summary>
		/// Cập nhật một danh sách dòng vào bảng.
		/// </summary>
		/// <param name="TObjects">Danh sách object kiểu T muốn cập nhật</param>
		public void UpdateAllOnSubmit(IEnumerable<T> TObjects)
		{
			foreach (T TObject in TObjects)
			{
				if (!Updateds.Contains(TObject))
				{
					Updateds.Add(TObject);
				}
			}
		}

		/// <summary>
		/// Xóa một dòng trong bảng.
		/// </summary>
		/// <param name="TObject">Object kiểu T muốn xóa</param>
		public void DeleteOnSubmit(T TObject)
		{
			if (!Deleteds.Contains(TObject))
			{
				Deleteds.Add(TObject);
			}
		}

		/// <summary>
		/// Xóa tất cả các dòng trong bảng.
		/// </summary>
		public void DeleteOnSubmit()
		{
			DeleteOnSubmit("1 = 1");
		}

		/// <summary>
		/// Cập nhật tất cả các dòng thỏa mản điều kiện trong bảng.
		/// </summary>
		/// <param name="WhereClause">Điều kiện muốn cập nhật (ex: ID = 1 AND NAME = 'ABC')</param>
		public void DeleteOnSubmit(string WhereClause)
		{
			TableBase TObject = Activator.CreateInstance<T>() as TableBase;
			if (TObject != null)
			{
				DeletedSQLs.Add(TObject.DeleteStatement(WhereClause));
			}
		}

		/// <summary>
		/// Xóa một danh sách dòng trong bảng.
		/// </summary>
		/// <param name="TObjects">Danh sách object kiểu T muốn xóa</param>
		public void DeleteAllOnSubmit(IEnumerable<T> TObjects)
		{
			foreach (T TObject in TObjects)
			{
				if (!Deleteds.Contains(TObject))
				{
					Deleteds.Add(TObject);
				}
			}
		}

		/// <summary>
		/// Thực hiện các thao tác Thêm/Sửa/Xóa một danh sách theo trạng thái.
		/// </summary>
		/// <param name="TObjects">Danh sách object kiểu T </param>
		public void AttachAllOnSubmit(IEnumerable<T> TObjects)
		{
			foreach (T TObject in TObjects)
			{
				switch ((TObject as TableBase).DataStatus)
				{
					case DBStatus.Inserted:
						InsertOnSubmit(TObject);
						break;
					case DBStatus.Updated:
						UpdateOnSubmit(TObject);
						break;
					case DBStatus.Deleted:
						DeleteOnSubmit(TObject);
						break;
				}
			}
		}

		/// <summary>
		/// Thực hiện cập nhật dữ liệu vào DB.
		/// </summary>
		/// <param name="trans">DB Trasaction</param>
		/// <returns>Số dòng thay đổi</returns>
		public int SubmitAll(IDbTransaction trans)
		{
			int intReturn = 0;
			foreach (T TObject in Deleteds)
			{
				(TObject as TableBase).DataManagement = this.DataManagement;
				intReturn += DeleteData(TObject, string.Empty, m_Connection, trans);
				(TObject as TableBase).DataStatus = DBStatus.Deleted;
			}
			foreach (string sql in DeletedSQLs)
			{
				intReturn += DeleteData(sql, m_Connection, trans);
			}
			foreach (T TObject in Inserteds)
			{
				(TObject as TableBase).DataManagement = this.DataManagement;
				intReturn += InsertData(TObject, m_Connection, trans);
				(TObject as TableBase).DataStatus = DBStatus.Normal;
			}
			foreach (T TObject in Updateds)
			{
				(TObject as TableBase).DataManagement = this.DataManagement;
				intReturn += UpdateData(TObject, string.Empty, string.Empty, m_Connection, trans);
				(TObject as TableBase).DataStatus = DBStatus.Normal;
			}
			foreach (string sql in UpdatedSQLs)
			{
				intReturn += UpdateData(sql, m_Connection, trans);
			}
			Deleteds.Clear();
			DeletedSQLs.Clear();
			Updateds.Clear();
			UpdatedSQLs.Clear();
			Inserteds.Clear();
			return intReturn;
		}

		/// <summary>
		/// Thực hiện thêm dữ liệu vào DB.
		/// </summary>
		/// <param name="TObject">T object muốn thêm</param>
		/// <param name="conn">Connection đến DB</param>
		/// <param name="trans">DB Trasaction</param>
		/// <returns>Số dòng thêm vào</returns>
		public int InsertData(T TObject, IDbConnection conn, IDbTransaction trans)
		{
			TableBase objBase = TObject as TableBase;
			if (!objBase.IsView)
			{
				objBase.PropertyChanged += OnPropertyChanged_Handler;
				return clsDAL.AffectData(objBase.InsertStatement(), conn, trans, this.DataManagement, objBase.InsertParameters());
			}
			return 0;
		}

		/// <summary>
		/// Thực hiện cập nhật dữ liệu vào DB.
		/// </summary>
		/// <param name="sql">Câu SQL muốn cập nhật</param>
		/// <param name="conn">Connection đến DB</param>
		/// <param name="trans">DB Trasaction</param>
		/// <returns>Số dòng được cập nhât</returns>
		public int UpdateData(string sql, IDbConnection conn, IDbTransaction trans)
		{
			if (!string.IsNullOrEmpty(sql))
			{
				return clsDAL.AffectData(sql, conn, trans,this.DataManagement);
			}
			return 0;
		}

		/// <summary>
		/// Thực hiện cập nhật dữ liệu vào DB.
		/// </summary>
		/// <param name="TObject">T object muốn cập nhật</param>
		/// <param name="Fields">Các trường muốn cập nhật (ex: "ID = 1, NAME = 'ABC'")</param>
		/// <param name="WhereClause">Điều kiện muốn cập nhật (ex: "ID = 1 AND NAME = 'ABC'")</param>
		/// <param name="conn">Connection đến DB</param>
		/// <param name="trans">DB Trasaction</param>
		/// <returns>Số dòng được cập nhât</returns>
		public int UpdateData(T TObject, string Fields, string WhereClause, IDbConnection conn, IDbTransaction trans)
		{
			TableBase objBase = TObject as TableBase;
			if (!objBase.IsView)
			{
				if (string.IsNullOrEmpty(Fields))
				{
					if (string.IsNullOrEmpty(WhereClause))
					{
						if (objBase != null)
						{
							if (!objBase.IsView)
							{
								return clsDAL.AffectData(objBase.UpdateStatement(), conn, trans, objBase.UpdateParameters());
							}
						}
					}
					else
					{
						if (objBase != null)
						{
							if (!objBase.IsView)
							{
								return clsDAL.AffectData(objBase.UpdateStatement(WhereClause), conn, trans, objBase.UpdateParameters());
							}
						}
					}
				}
				else
				{
					return clsDAL.AffectData(objBase.UpdateStatement(Fields, WhereClause), conn, trans, this.DataManagement, objBase.UpdateParameters());
				}
			}
			return 0;
		}

		/// <summary>
		/// Thực hiện xóa dữ liệu khỏi DB.
		/// </summary>
		/// <param name="sql">Câu lệnh SQL xóa</param>
		/// <param name="conn">Connection đến DB</param>
		/// <param name="trans">DB Trasaction</param>
		/// <returns>Số dòng bị xóa</returns>
		public int DeleteData(string sql, IDbConnection conn, IDbTransaction trans)
		{
			if (!string.IsNullOrEmpty(sql))
			{
				return clsDAL.AffectData(sql, conn, trans, this.DataManagement);
			}
			return 0;
		}

		/// <summary>
		/// Thực hiện xóa dữ liệu khỏi DB.
		/// </summary>
		/// <param name="TObject">T object muốn xóa</param>
		/// <param name="WhereClause">Điều kiện muốn xóa (ex: "ID = 1 AND NAME = 'ABC'")</param>
		/// <param name="conn">Connection đến DB</param>
		/// <param name="trans">DB Trasaction</param>
		/// <returns>Số dòng bị xóa</returns>
		public int DeleteData(T TObject, string WhereClause, IDbConnection conn, IDbTransaction trans)
		{
			TableBase objBase = TObject as TableBase;
			if (string.IsNullOrEmpty(WhereClause))
			{
				if (objBase != null)
				{
					if (!objBase.IsView)
					{
						return clsDAL.AffectData(objBase.DeleteStatement(), conn, trans, this.DataManagement, objBase.DeleteParameters());
					}
				}
			}
			else
			{
				objBase = objBase ?? (Activator.CreateInstance<T>() as TableBase);
				if (!objBase.IsView)
				{
					return clsDAL.AffectData(objBase.DeleteStatement(WhereClause), conn, trans, this.DataManagement, objBase.DeleteParameters());
				}
			}
			return 0;
		}
	}
}