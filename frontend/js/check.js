let selectedDrugs = [];

const demoDiseases = [
    { id: 1, diseaseName: "Viêm loét dạ dày" },
    { id: 2, diseaseName: "Hen suyễn" },
    { id: 3, diseaseName: "Tăng huyết áp" },
    { id: 4, diseaseName: "Suy gan" },
    { id: 5, diseaseName: "Suy thận" }
];

document.addEventListener("DOMContentLoaded", function () {
    loadDiseases();

    document.getElementById("addDrugBtn").addEventListener("click", addDrug);
    document.getElementById("checkContraBtn").addEventListener("click", checkContraindication);
    document.getElementById("checkInteractionBtn").addEventListener("click", checkInteraction);
});

async function loadDiseases() {
    const diseaseSelect = document.getElementById("diseaseSelect");

    try {
        const diseases = await apiRequest("/Disease");

        diseases.forEach(disease => {
            const option = document.createElement("option");
            option.value = disease.diseaseName;
            option.textContent = disease.diseaseName;
            diseaseSelect.appendChild(option);
        });

    } catch {
        demoDiseases.forEach(disease => {
            const option = document.createElement("option");
            option.value = disease.diseaseName;
            option.textContent = disease.diseaseName;
            diseaseSelect.appendChild(option);
        });
    }
}

function addDrug() {
    clearMessage("checkMessage");

    const drugInput = document.getElementById("drugInput");
    const drugName = drugInput.value.trim();

    if (!drugName) {
        showMessage("checkMessage", "Vui lòng nhập tên thuốc.");
        return;
    }

    if (hasSpecialDangerousChars(drugName)) {
        showMessage("checkMessage", "Tên thuốc không được chứa ký tự đặc biệt nguy hiểm.");
        return;
    }

    if (selectedDrugs.includes(drugName)) {
        showMessage("checkMessage", "Thuốc này đã được thêm.");
        return;
    }

    selectedDrugs.push(drugName);
    drugInput.value = "";

    renderDrugList();
}

function renderDrugList() {
    const drugList = document.getElementById("drugList");
    drugList.innerHTML = "";

    selectedDrugs.forEach((drug, index) => {
        const item = document.createElement("div");
        item.className = "drug-item";

        item.innerHTML = `
            <span>${drug}</span>
            <button class="btn btn-danger" onclick="removeDrug(${index})">Xóa</button>
        `;

        drugList.appendChild(item);
    });
}

function removeDrug(index) {
    selectedDrugs.splice(index, 1);
    renderDrugList();
}

async function checkContraindication() {
    clearMessage("checkMessage");

    const diseaseName = document.getElementById("diseaseSelect").value;

    if (selectedDrugs.length === 0) {
        showMessage("checkMessage", "Vui lòng nhập ít nhất một thuốc.");
        return;
    }

    if (!diseaseName) {
        showMessage("checkMessage", "Vui lòng chọn bệnh nền.");
        return;
    }

    const requestData = {
        drugNames: selectedDrugs,
        diseaseNames: [diseaseName]
    };

    try {
        const result = await apiRequest("/Check/contraindication", "POST", requestData);

        saveResult("contraindication", requestData, result);

    } catch {
        const demoResult = {
            hasWarning: true,
            level: "High",
            message: `${selectedDrugs.join(", ")} có thể không phù hợp với bệnh nền ${diseaseName}.`,
            recommendations: ["Paracetamol"]
        };

        saveResult("contraindication", requestData, demoResult);
    }
}

async function checkInteraction() {
    clearMessage("checkMessage");

    if (selectedDrugs.length < 2) {
        showMessage("checkMessage", "Cần nhập ít nhất 2 thuốc để kiểm tra tương tác.");
        return;
    }

    const requestData = {
        drugNames: selectedDrugs
    };

    try {
        const result = await apiRequest("/Check/interaction", "POST", requestData);

        saveResult("interaction", requestData, result);

    } catch {
        const demoResult = {
            hasWarning: true,
            level: "Medium",
            message: `Có khả năng xảy ra tương tác giữa ${selectedDrugs.join(" và ")}.`,
            recommendations: ["Tham khảo ý kiến bác sĩ trước khi sử dụng."]
        };

        saveResult("interaction", requestData, demoResult);
    }
}

function saveResult(type, input, result) {
    const data = {
        type: type,
        input: input,
        result: result,
        createdAt: new Date().toLocaleString("vi-VN")
    };

    localStorage.setItem("lastResult", JSON.stringify(data));

    const history = JSON.parse(localStorage.getItem("historyItems") || "[]");
    history.unshift(data);
    localStorage.setItem("historyItems", JSON.stringify(history));

    window.location.href = "result.html";
}