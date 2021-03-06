﻿using System;
using KdSoft.Lmdb.Interop;

namespace KdSoft.Lmdb
{
    /// <summary>
    /// Read-only transaction. There can be multiple simultaneously active read-only transactions but only one that can write.
    /// If you want to pass read-only transactions across threads, you can use the MDB_NOTLS option on the environment.
    /// </summary>
    public class ReadOnlyTransaction: Transaction
    {
        internal ReadOnlyTransaction(IntPtr txn, Transaction parent, Action<IntPtr> disposed) : base(txn, parent, disposed) {
            //
        }

        /// <summary>
        /// Reset a read-only transaction.
        /// Abort the transaction like mdb_txn_abort(), but keep the transaction handle. mdb_txn_renew() may reuse the handle.
        /// This saves allocation overhead if the process will start a new read-only transaction soon, and also locking overhead
        /// if MDB_NOTLS is in use.The reader table lock is released, but the table slot stays tied to its thread or MDB_txn.
        /// Use mdb_txn_abort() to discard a reset handle, and to free its lock table slot if MDB_NOTLS is in use.
        /// Cursors opened within the transaction must not be used again after this call, except with mdb_cursor_renew().
        /// Reader locks generally don't interfere with writers, but they keep old versions of database pages allocated.
        /// Thus they prevent the old pages from being reused when writers commit new data, and so under heavy load
        /// the database size may grow much more rapidly than otherwise.
        /// </summary>
        public void Reset() {
            var handle = CheckDisposed();
            DbLib.mdb_txn_reset(handle);
        }

        /// <summary>
        /// Renew a read-only transaction.
        /// This acquires a new reader lock for a transaction handle that had been released by mdb_txn_reset().
        /// It must be called before a reset transaction may be used again.
        /// </summary>
        public void Renew() {
            var handle = CheckDisposed();
            var ret = DbLib.mdb_txn_renew(handle);
            ErrorUtil.CheckRetCode(ret);
        }
    }
}
