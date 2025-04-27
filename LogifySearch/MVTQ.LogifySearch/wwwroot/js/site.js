// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ReloadBootstrapTable(id) {
    $(`#${id}`).bootstrapTable('destroy')
    $(`#${id}`).bootstrapTable()
}
function InitChosenSelect(id) {
    $(`#${id}`).chosen(
        {
            no_results_text: "Oops, nothing found!",
            width: "100%"
        }
    )
}