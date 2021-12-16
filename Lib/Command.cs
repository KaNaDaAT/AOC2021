using System;
using System.Collections.Generic;
using System.Text;

namespace Lib {
	public interface Command {

		public T Run<T>();

	}
}
