﻿// 
//   MarkdownDeep - http://www.toptensoftware.com/markdowndeep
//	 Copyright (C) 2010-2011 Topten Software
// 
//   Licensed under the Apache License, Version 2.0 (the "License"); you may not use this product except in 
//   compliance with the License. You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software distributed under the License is 
//   distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//   See the License for the specific language governing permissions and limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkdownDeep {
	internal class TableCellDefinition {
		private string _content;
		private ColumnAlignment _alignment;
		private int _colSpan;
		private int _rowSpan;
		private CellStyle _cellStyle;

		public TableCellDefinition() : this(null, ColumnAlignment.NA, 1, 1, CellStyle.TD) {
			
		}
		public TableCellDefinition(string content, ColumnAlignment alignment, int colSpan, int rowSpan, CellStyle cellStyle) {
			this._content = content;
			this._alignment = alignment;
			this._colSpan = colSpan;
			this._rowSpan = rowSpan;
			this._cellStyle = cellStyle;
		}

		public string Content {
			get { return this._content; }
			set { this._content = value; }
		}

		public ColumnAlignment Alignment {
			get { return this._alignment; }
			set { this._alignment = value; }
		}

		public int ColSpan {
			get { return this._colSpan; }
			set { if (value > 0) this._colSpan = value; }
		}

		public int RowSpan {
			get { return this._rowSpan; }
			set { if (value > 0) this._rowSpan = value; }
		}

		public CellStyle CellStyle {
			get { return this._cellStyle; }
			set { this._cellStyle = value; }
		}

		public string TagName {
			get {
				switch (this._cellStyle) {
					case CellStyle.TH:
						return "th";
					case CellStyle.TD:
						return "td";
				}
				return null;
			}
		}

		public void RenderOpenTag(StringBuilder b, string tagName, ColumnAlignment alignment) {
			b.Append("<");
			b.Append(tagName ?? this.TagName);
			var alig = (alignment != ColumnAlignment.NA ? alignment : this.Alignment);
			switch (alignment) {
				case ColumnAlignment.Left:
					b.Append(" align=\"left\"");
					break;
				case ColumnAlignment.Right:
					b.Append(" align=\"right\"");
					break;
				case ColumnAlignment.Center:
					b.Append(" align=\"center\"");
					break;
			}
			if (this.ColSpan > 1) {
				b.Append(" colspan=\"");
				b.Append(this.ColSpan);
				b.Append("\"");
			}
			if (this.RowSpan > 1) {
				b.Append(" rowspan=\"");
				b.Append(this.RowSpan);
				b.Append("\"");
			}
			b.Append(">");
		}
		public void RenderCloseTag(StringBuilder b, string tagName) {
			b.Append("</");
			b.Append(tagName ?? this.TagName);
			b.Append(">");
		}
	}
	internal enum CellStyle {
		TD		// normal table cell
		, TH	// head table cell
	}
}