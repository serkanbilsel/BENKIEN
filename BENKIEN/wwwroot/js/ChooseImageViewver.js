function displaySelectedFiles() {
    const input = document.querySelector('input[type="file"]');
    const files = input.files;
    const infoDiv = document.getElementById('selectedFilesInfo');
    infoDiv.innerHTML = "";

    if (files.length > 0) {
        for (let i = 0; i < files.length; i++) {
            const container = document.createElement('div');
            container.className = 'm-2 position-relative'; // İsterseniz aralara boşluk ekleyebilirsiniz

            const image = document.createElement('img');
            image.src = URL.createObjectURL(files[i]); // Dosyayı URL'den okuyarak önizleme yapar
            image.width = 100; // İsterseniz genişliği ve yüksekliği ayarlayabilirsiniz
            container.appendChild(image);

            const closeIcon = document.createElement('span');
            closeIcon.className = 'position-absolute top-0 end-0 m-2 cursor-pointer close-icon';
            closeIcon.innerHTML = '&times;';
            closeIcon.onclick = function () {
                // İlgili resmi kaldır
                container.remove();

                // Seçilen dosyadan da kaldır
                const index = Array.from(input.files).findIndex(file => file === files[i]);
                input.files = new FileListSubset(input.files, index);
            };
            container.appendChild(closeIcon);

            infoDiv.appendChild(container);
        }
    } else {
        infoDiv.textContent = "Seçili dosya yok.";
    }
}

class FileListSubset extends FileList {
    constructor(files, indexToRemove) {
        const subset = Array.from(files).filter((_, index) => index !== indexToRemove);
        super(...subset);
    }
}