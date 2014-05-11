﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NDbfReaderEx
{
  public abstract class MemoFileBase : IMemoFile, IDisposable
  {
    protected Stream   stream;
    protected Encoding encoding;

    public   bool     disposed   { get; private set;}


    internal MemoFileBase(Stream stream, Encoding encoding)
    {
      disposed = false;

      if (stream == null)
      {
        throw ExceptionFactory.CreateArgumentException("stream", "MemoFileXXXX/stream is null!");
      }

      if (encoding == null)
      {
        throw ExceptionFactory.CreateArgumentException("encoding", "MemoFileXXXX/encoding is null!");
      }

      this.stream   = stream;
      this.encoding = encoding;
    }


    #region IMemoFile Members

    public abstract string ReadMemoText(int blockNo);

    public abstract int WriteMemoText(string memoText, int oldBlockNo = 0);
    
    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      if (! disposed)
      {
        disposed = true;
        this.stream.Dispose();        
      }
    }

    ~MemoFileBase()
    { // Destructor implemented because if 'DbtTable.memoStreamCloseWhenDispose == false' it is necessary to close stream
      Dispose();
    }
    #endregion
  }
}
