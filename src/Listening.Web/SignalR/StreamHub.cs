﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Listening.Web.SignalR
{
    public class StreamHub : Hub
    {
        //public ChannelReader<int> Counter(
        //int count,
        //int delay,
        //CancellationToken cancellationToken)
        //{
        //    var channel = Channel.CreateUnbounded<int>();

        //    // We don't want to await WriteItemsAsync, otherwise we'd end up waiting
        //    // for all the items to be written before returning the channel back to
        //    // the client.
        //    _ = WriteItemsAsync(channel.Writer, count, delay, cancellationToken);

        //    return channel.Reader;
        //}

        //private async Task WriteItemsAsync(
        //    ChannelWriter<int> writer,
        //    int count,
        //    int delay,
        //    CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        for (var i = 0; i < count; i++)
        //        {
        //            // Check the cancellation token regularly so that the server will stop
        //            // producing items if the client disconnects.
        //            cancellationToken.ThrowIfCancellationRequested();
        //            await writer.WriteAsync(i);

        //            // Use the cancellationToken in other APIs that accept cancellation
        //            // tokens so the cancellation can flow down to them.
        //            await Task.Delay(delay, cancellationToken);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        writer.TryComplete(ex);
        //    }

        //    writer.TryComplete();
        //}

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task Send(Stream message)
        {
            await Clients.Others.SendAsync("Send", message);
        }
    }
}
