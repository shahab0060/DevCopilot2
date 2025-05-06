const openCameraBtn = document.getElementById('openCameraBtn');
const video = document.getElementById('video');
const permissionMessage = document.getElementById('permissionMessage');
var modelViewer = document.getElementById('model');

async function checkCameraPermissions() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ video: true });
        stream.getTracks().forEach(track => track.stop());
        return true;
    } catch (error) {
        return false;
    }
}

async function openCamera() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: 'environment' } });
        video.srcObject = stream;
        video.style.display = 'block';
        modelViewer.style.display = 'block';
        openCameraBtn.style.display = 'none';
        permissionMessage.style.display = 'none';
    } catch (error) {
        permissionMessage.style.display = 'block';
    }
}

openCameraBtn.addEventListener('click', async () => {
    const hasPermission = await checkCameraPermissions();
    if (hasPermission) {
        openCamera();
    } else {
        permissionMessage.style.display = 'block';
        openCamera();
    }
});

document.addEventListener('DOMContentLoaded', async () => {
    const hasPermission = await checkCameraPermissions();
    if (hasPermission) {
        openCameraBtn.click();
    }
});

document.getElementById('tipsButton').addEventListener('click', function () {
    const tipsContainer = document.getElementById('tipsContainer');
    const tipsButton = document.getElementById('tipsButton');

    tipsContainer.classList.toggle('d-none');
    tipsButton.classList.add('flip');

    setTimeout(() => {
        tipsButton.classList.remove('flip');
        tipsButton.innerHTML = tipsButton.innerHTML.includes('question-circle') ? '<i class="bi bi-x-circle"></i>' : '<i class="bi bi-question-circle"></i>';
    }, 300); // Half of the animation duration
});

document.getElementById('closeTipsButton').addEventListener('click', function () {
    const tipsContainer = document.getElementById('tipsContainer');
    const tipsButton = document.getElementById('tipsButton');

    tipsContainer.classList.add('d-none');
    tipsButton.classList.add('flip');

    setTimeout(() => {
        tipsButton.classList.remove('flip');
        tipsButton.innerHTML = '<i class="bi bi-question-circle"></i>';
    }, 300); // Half of the animation duration
});

document.addEventListener('click', function (event) {
    if (!event.target.closest('#tipsButton') && !event.target.closest('#tipsContainer')) {
        const tipsContainer = document.getElementById('tipsContainer');
        const tipsButton = document.getElementById('tipsButton');

        if (!tipsContainer.classList.contains('d-none')) {
            tipsContainer.classList.add('d-none');
            tipsButton.classList.add('flip');

            setTimeout(() => {
                tipsButton.classList.remove('flip');
                tipsButton.innerHTML = '<i class="bi bi-question-circle"></i>';
            }, 300); // Half of the animation duration
        }
    }
});
