document.addEventListener("DOMContentLoaded", function () {
    loadHistory();
});

function loadHistory() {
    const historyTable = document.getElementById("historyTable");
    const history = JSON.parse(localStorage.getItem("historyItems") || "[]");

    if (history.length === 0) {
        historyTable.innerHTML = `
            <tr>
                <td colspan="4">Chưa có lịch sử tra cứu.</td>
            </tr>
        `;
        return;
    }

    historyTable.innerHTML = "";

    history.forEach(item => {
        const drugs = item.input.drugNames
            ? item.input.drugNames.join(", ")
            : "Không có dữ liệu";

        const typeText = item.type === "contraindication"
            ? "Chống chỉ định"
            : "Tương tác thuốc";

        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${item.createdAt}</td>
            <td>${typeText}</td>
            <td>${drugs}</td>
            <td>${item.result.message || "Không có kết quả"}</td>
        `;

        historyTable.appendChild(row);
    });
}