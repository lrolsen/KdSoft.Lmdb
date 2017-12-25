﻿using System;

namespace KdSoft.Lmdb
{
    public class Transaction: AbstractTransaction
    {
        internal Transaction(IntPtr handle, Transaction parent, Action<IntPtr> disposed) : base(handle, parent, disposed) {
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
            lock (rscLock) {
                var txn = CheckDisposed();
                Lib.mdb_txn_reset(txn);
            }
        }

        /// <summary>
        /// Renew a read-only transaction.
        /// This acquires a new reader lock for a transaction handle that had been released by mdb_txn_reset().
        /// It must be called before a reset transaction may be used again.
        /// </summary>
        public void Renew() {
            lock (rscLock) {
                var txn = CheckDisposed();
                var ret = Lib.mdb_txn_renew(txn);
                Util.CheckRetCode(ret);
            }
        }
    }
}
