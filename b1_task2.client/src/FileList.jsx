import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

const FileList = () => {
    const [files, setFiles] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchFiles = async () => {
            try {
                const response = await fetch(`api/Excel/files`, { method: 'GET' }); 
                if (!response.ok) {
                    throw new Error(`Ошибка: ${response.statusText}`);
                }
                const data = await response.json();
                setFiles(data); 
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };
        fetchFiles();
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    return (
        <div>
            <h1>Uploaded Files</h1>
            <ul>
                {files?.map(file => (
                    <li key={file.fileID} >
                        <div>
                            <Link to={`api/Excel/filedata/${file.fileID}`}>
                                {file.fileName}
                            </Link>
                        </div>
                    </li>
                ))}
            </ul>
            <Link to="/upload">Загрузить новый файл</Link>
        </div>
    );
};

export default FileList;
