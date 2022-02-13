using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarisKBT
{
	public interface IPdfSave
	{
		void Save(PdfDocument doc, string fileName);
	}
}
