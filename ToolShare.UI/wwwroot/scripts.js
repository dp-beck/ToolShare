function openWidget() {
    return new Promise((resolve) => {
        cloudinary.openUploadWidget(
            {
            cloudName: 'dzsqoueki',
            uploadPreset: 'toolshare',
            folder: 'widgetUpload',
            cropping: true, 
                croppingAspectRatio: 1
            }, 
            (error, result) => {
                if (!error && result && result.event === "success") {
                    console.log("Uploaded file URL:", result.info.secure_url);
                    resolve(result.info.secure_url);
                }
            });
    });
}