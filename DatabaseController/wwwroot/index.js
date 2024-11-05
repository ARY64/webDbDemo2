
var editable = false;

// Used by Tabulator to check if a cell is editable
var editCheck = function(cell){
    return editable;
}
// Toggles editable state and controls
var setEditable = function(bool){
    editable = bool;  
    document.getElementById("editGroup").disabled = !editable;
    document.getElementById("navGroup").disabled = editable;  
}
var table = new Tabulator("#orderItemTable", {
    height:500,
    ajaxType:"GET",
    layout:"fitColumns",
    columns:[
        // {title:"Order Item Id", field:"OrderItemId", editor:"input"},
        {title:"Order Id", field:"OrderId", editor:"input", editable:editCheck},
        {title:"Item Id", field:"ItemId", editor:"input", editable:editCheck},
        {title:"Price", field:"Price", editor:"input", editable:editCheck},
        {title:"Quantity", field:"Quantity", editor:"input", editable:editCheck},
        {title:"Position", field:"Position", editor:"input", editable:editCheck},
    ],    
});
var recentTable = new Tabulator("#recentTable", {
    height:500,
    ajaxURL:"api/Order/recent", // Set this to the URL of your script
    ajaxType:"GET", // Set this to the HTTP method your script expects
    layout:"fitColumns",
    columns:[
        {title:"RecentOrders", field:"OrderId", cellClick:function(e, cell){
            //get rows 
            var recentOrder = recentTable.getRows();
            //deselect rows
            recentOrder.forEach(function(row){
                row.deselect();
            });
            //get clicked row 
            var row = cell.getRow();
            //select clicked row    
            row.select();
            //get data from clicked row
            var data = cell.getData();
            //set data in table from API and fill in empty rows
            table.setData("api/OrderItem/Index/" + data.OrderId).then(function(){
                var rowCount = table.getRows().length;
                for(var i = 0; i < 18 - rowCount; i++){
                    table.addRow({});
                }
            });
            setEditable(false);
        }},
    ],  
});
setEditable(false);
// Auto add last row and set edited flag
table.on('cellEdited', (cell) => {
    var row = cell.getRow();
    var rows = table.getRows();
    //check if edited cell is in the last row
    if(row === rows[rows.length - 1]){
        table.addRow({});
    }
    row.edited = true;
})

    


// Select the buttons
var editButton = document.querySelector('#edit-btn');
var newButton = document.querySelector('#new-btn');
var nextButton = document.querySelector('#next-btn');
var previousButton = document.querySelector('#previous-btn');
var saveButton = document.querySelector('#save-btn');
var cancelButton = document.querySelector('#cancel-btn');


editButton.addEventListener('click', function() {
    setEditable(true);
    });

// newButton.addEventListener('click', function() {
//     table.setData([]);
//     setEditable(true);
// });

// Add event listener
saveButton.addEventListener('click', function() {
    // Get edited rows  
    var data = table.getData().filter(row => row.edited);
    // Send data to server
    fetch('api/OrderItem/Edit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
    .then(response => {
        // Check if the response body is empty
        if (response.headers.get('content-length') === '0' || response.status === 204) {
            return {};
        } else {
            return response.json();
        }
    });
});