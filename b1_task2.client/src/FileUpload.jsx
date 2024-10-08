import React, { useState } from 'react';

const FileUpload = () => {
    const [selectedFile, setSelectedFile] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [successMessage, setSuccessMessage] = useState('');

    const handleFileChange = (event) => {
        setSelectedFile(event.target.files[0]);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (!selectedFile) {
            setError('Пожалуйста, выберите файл для загрузки.');
            return;
        }

        const formData = new FormData();
        formData.append('inputfile', selectedFile);

        setLoading(true);
        setError(null);
        setSuccessMessage('');

        try {
            const response = await fetch('api/Excel', {
                method: 'POST',
                body: formData
                
            });

            if (!response.ok) {
                throw new Error(`Ошибка: ${response.statusText}`);
            }

            setSuccessMessage('Файл успешно загружен!');
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h1>Загрузить файл</h1>
            <form onSubmit={handleSubmit}>
                <input type="file" onChange={handleFileChange} />
                <button type="submit" disabled={loading}>
                    {loading ? 'Загрузка...' : 'Загрузить'}
                </button>
            </form>
            {error && <div style={{ color: 'red' }}>Ошибка: {error}</div>}
            {successMessage && <div style={{ color: 'green' }}>{successMessage}</div>}
        </div>
    );
};

export default FileUpload;