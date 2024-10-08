import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

// Компонент для отображения баланса одного аккаунта(б/сч)
const AccountBalance = ({ balance }) => (
    <tr>
        <td>{balance.accountNumber}</td>
        <td align='right'>{balance.incomingActive}</td>
        <td align='right'>{balance.incomingPassive}</td>
        <td align='right'>{balance.debit}</td>
        <td align='right'>{balance.credit}</td>
        <td align='right'>{balance.outgoingActive}</td>
        <td align='right'>{balance.outgoingPassive}</td>
    </tr>
);
// Компонент для отображения баланса класса(и файла)
const ClassBalance = ({ balance, text }) => (
    <tr>
        <th>{text}</th>
        <th align='right'>{balance.incomingActive}</th>
        <th align='right'>{balance.incomingPassive}</th>
        <th align='right'>{balance.debit}</th>
        <th align='right'>{balance.credit}</th>
        <th align='right'>{balance.outgoingActive}</th>
        <th align='right'>{balance.outgoingPassive}</th>
    </tr>
);

// Компонент для отображения информации по классу
const ClassInfo = ({ classData }) => (
    <tbody>
        <tr>
        <th align='center' colSpan={8}>{classData.class.className}</th>
        </tr>
        {classData.accountBalances.map(accountBalance => (
            <AccountBalance key={accountBalance.accountID} balance={accountBalance} />
        ))}
        <ClassBalance balance={classData.classBalance} text="По классу" />
    </tbody>
);

// Главный компонент, который будет загружать и отображать данные
const FileData = () => {
    const { fileID } = useParams();

    const [fileData, setFileData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchFileData = async () => {
            try {
                const response = await fetch(`${fileID}`, { method: 'GET' });
                if (!response.ok) {
                    throw new Error(`Ошибка: ${response.statusText}`);
                }
                const data = await response.json();
                setFileData(data);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };
        fetchFileData();
    }, [fileID]);


    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error fetching data</p>;

    return (
        <div>
            <p>{fileData.file.fileInfoDetails}</p>
            <table border='1px' solid='#000'>
                <thead>
                    <tr>
                        <th>Б/сч</th>
                        <th align='center' colSpan={2}>Входящее сальдо</th>
                        <th align='center' colSpan={2}>Обороты</th>
                        <th align='center' colSpan={2}>Исходящее сальдо</th>
                    </tr>
                    <tr>
                        <th></th>
                        <th>Актив</th>
                        <th>Пассив</th>
                        <th>Дебет</th>
                        <th>Кредит</th>
                        <th>Актив</th>
                        <th>Пассив</th>
                    </tr>
                </thead>
                
                {fileData.classes.map(classData => (
                    <ClassInfo key={classData.class.classID} classData={classData} />
                ))}
                 <ClassBalance balance={fileData.fileBalance} text="По файлу"/>
            </table>
        </div>
    );
};

export default FileData;
