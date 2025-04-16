function dragstartHandler(ev) {
    const itemId = ev.target.getAttribute("data-item-id");
    ev.dataTransfer.setData("text", itemId);
}

function dragoverHandler(ev) {
    ev.preventDefault();
}

function dropHandler(ev) {
    ev.preventDefault();
    const data = ev.dataTransfer.getData("text");
    const draggedItem = document.querySelector(`[data-item-id="${data}"]`);
    const board = ev.target.closest('.board');
    const boardId = board.getAttribute("data-board-id");

    if (!board) return;

    board.prepend(draggedItem);

    $.post(`/order/${data}/status?sid=${boardId}`);

    console.log(`Moved item: ${data} to board: ${boardId}`);
}

$(".board-item").on("click", function () {
    const itemId = $(this).attr("data-item-id");
    console.log(itemId);
    $.get('/Order/edit', { oid: itemId }, function (data) {
        $('#editOrderBody').html(data);
        $('#editOrderModal').modal('show');
    });
})

