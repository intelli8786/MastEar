import numpy as np
import tensorflow as tf
import librosa
from flask import Flask, request
import os


def extract_feature(file_name):
    X, sample_rate = librosa.load(file_name)
    stft = np.abs(librosa.stft(X))
    mfccs = np.mean(librosa.feature.mfcc(y=X, sr=sample_rate, n_mfcc=40).T,axis=0)
    chroma = np.mean(librosa.feature.chroma_stft(S=stft, sr=sample_rate).T,axis=0)
    mel = np.mean(librosa.feature.melspectrogram(X, sr=sample_rate).T,axis=0)
    contrast = np.mean(librosa.feature.spectral_contrast(S=stft, sr=sample_rate).T,axis=0)
    tonnetz = np.mean(librosa.feature.tonnetz(y=librosa.effects.harmonic(X), sr=sample_rate).T,axis=0)
    return mfccs,chroma,mel,contrast,tonnetz

n_dim = 193
n_classes = 6
n_hidden_units_one = 300
n_hidden_units_two = 200
n_hidden_units_three = 100
sd = 1 / np.sqrt(n_dim)
sound_names = ["Cooler_foil ","Cooler_HindrancePen","Cooler_Normal","Fen","Motor_Normal","Motor_Hindrance"]

X = tf.placeholder(tf.float32,[None,n_dim])
Y = tf.placeholder(tf.float32,[None,n_classes])

W_1 = tf.Variable(tf.random_normal([n_dim, n_hidden_units_one], mean=0, stddev=sd), name="w1")
b_1 = tf.Variable(tf.random_normal([n_hidden_units_one], mean=0, stddev=sd), name="b1")
h_1 = tf.nn.sigmoid(tf.matmul(X, W_1) + b_1)

W_2 = tf.Variable(tf.random_normal([n_hidden_units_one, n_hidden_units_two], mean=0, stddev=sd), name="w2")
b_2 = tf.Variable(tf.random_normal([n_hidden_units_two], mean=0, stddev=sd), name="b2")
h_2 = tf.nn.tanh(tf.matmul(h_1, W_2) + b_2)

W_3 = tf.Variable(tf.random_normal([n_hidden_units_two, n_hidden_units_three], mean=0, stddev=sd), name="w3")
b_3 = tf.Variable(tf.random_normal([n_hidden_units_three], mean=0, stddev=sd), name="b3")
h_3 = tf.nn.sigmoid(tf.matmul(h_2, W_3) + b_3)

W = tf.Variable(tf.random_normal([n_hidden_units_three, n_classes], mean=0, stddev=sd), name="w")
b = tf.Variable(tf.random_normal([n_classes], mean = 0, stddev=sd), name="b")
z = tf.matmul(h_3, W) + b
y_sigmoid = tf.nn.sigmoid(z)
y_ = tf.nn.softmax(z)

init = tf.initialize_all_variables()

saver = tf.train.Saver()
sess = tf.Session()
sess.run(init)
saver.restore(sess, 'model.ckpt')

app = Flask(__name__)
app.config['UPLOAD_FOLDER'] = '/mnt'
app.config['MAX_CONTENT_LENGTH'] = 16 * 1024 * 1024


@app.route('/hello', methods=['POST'])
def upload_file():
    if 'file' not in request.files:
        return ''
    file = request.files['file']
    if file.filename == '':
        return ''
    audio_file = os.path.join(app.config['UPLOAD_FOLDER'], file.filename)
    file.save(audio_file)
    mfccs, chroma, mel, contrast,tonnetz = extract_feature(audio_file)
    x_data = np.hstack([mfccs,chroma,mel,contrast,tonnetz])
    y_hat, sigmoid = sess.run([y_, y_sigmoid], feed_dict={X: x_data.reshape(1,-1)})
    index = np.argmax(y_hat)
    print(y_hat)
    print(y_)
    return '%s' % (sound_names[index])
