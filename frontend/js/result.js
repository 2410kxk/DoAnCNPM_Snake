document.addEventListener("DOMContentLoaded", function () {
    const resultBox = document.getElementById("resultBox");
    const data = localStorage.getItem("lastResult");

    if (!data) {
        resultBox.innerHTML = `
            <div class="result-card">
                <p>Chưa có kết quả kiểm tra.</p>
            </div>
        `;
        return;
    }

    const resultData = JSON.parse(data);
    const result = resultData.result;

    let levelClass = "level-low";

    if (result.level === "Medium") {
        levelClass = "level-medium";
    }

    if (result.level === "High") {
        levelClass = "level-high";
    }

    const drugs = resultData.input.drugNames
        ? resultData.input.drugNames.join(", ")
        : "Không có dữ liệu";

    const diseases = resultData.input.diseaseNames
        ? resultData.input.diseaseNames.join(", ")
        : "Không áp dụng";

    const recommendations = result.recommendations || result.recommendedDrugs || [];

    resultBox.innerHTML = `
        <div class="result-card">
            <h3>Thông tin kiểm tra</h3>
            <p><strong>Loại kiểm tra:</strong> ${resultData.type === "contraindication" ? "Chống chỉ định" : "Tương tác thuốc"}</p>
            <p><strong>Thuốc:</strong> ${drugs}</p>
            <p><strong>Bệnh nền:</strong> ${diseases}</p>
            <p><strong>Thời gian:</strong> ${resultData.createdAt}</p>
        </div>

        <div class="result-card">
            <h3>Kết quả</h3>
            <p><strong>Mức độ:</strong> <span class="${levelClass}">${result.level || "Không xác định"}</span></p>
            <p><strong>Cảnh báo:</strong> ${result.message || "Không có cảnh báo."}</p>
        </div>

        <div class="result-card">
            <h3>Gợi ý</h3>
            ${
                recommendations.length > 0
                    ? `<ul>${recommendations.map(item => `<li>${item}</li>`).join("")}</ul>`
                    : `<p>Không có gợi ý thay thế.</p>`
            }
        </div>
    `;
});