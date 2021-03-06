﻿using Lacuna.RestPki.Api;
using Lacuna.RestPki.Client;
using SampleSite.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lacuna.RestPki.SampleSite.Controllers {

	public class OpenPadesSignatureController : Controller {

		/**
		 *	This action submits a PDF file to Rest PKI for inspection of its signatures.
		 */
		[HttpGet]
		public ActionResult Index(string userfile) {

			// Our action only works if a userfile is given to work with.
			if (string.IsNullOrEmpty(userfile)) {
				return HttpNotFound();
			}
            var filename = userfile.Replace("_", ".");
            // Note: we're receiving the userfile argument with "_" as "." because of limitations of
            // ASP.NET MVC.

            // Get an instance of the PadesSignatureExplorer class, used to open/validate PDF signatures.
            var sigExplorer = new PadesSignatureExplorer(Util.GetRestPkiClient()) {
                // Specify that we want to validate the signatures in the file, not only inspect them.
                Validate = true,
                // Specify the parameters for the signature validation:
                // Accept any PAdES signature as long as the signer has an ICP-Brasil certificate.
                DefaultSignaturePolicyId = StandardPadesSignaturePolicies.Basic,
                // We have encapsulated the security context choice on Util.cs.
                SecurityContextId = Util.GetSecurityContextId()
            };

			// Set the PDF file
			sigExplorer.SetSignatureFile(Server.MapPath("~/App_Data/" + filename));

            // Call the Open() method, which returns the signature file's information
            var signature = sigExplorer.Open();

            // Render the information (see file Views/OpenPadesSignature/Index.html for more information on
            // the information returned)
			return View(signature);
		}
	}
}
