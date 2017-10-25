﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms {
	public partial class Download : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			var filename = Request.QueryString["file"];
			var path = String.Empty;

			if (Request.QueryString["from"] == "content") {
				path = Server.MapPath(string.Format("~/Content/{0}", filename));
			} else {
				path = Server.MapPath(string.Format("~/App_Data/{0}", filename));
			}
			Response.ContentType = MimeMapping.GetMimeMapping(filename);
			Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
			Response.WriteFile(path);
			Response.End();
		}
	}
}